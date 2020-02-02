﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GenericEntityController {

	[SerializeField][ReadOnly]
	Transform plusRoot;

	public Transform PlusRoot
	{
		get
		{
			return plusRoot;
		}
	}

	[SerializeField]
	PlayerStyle playerStyle;

	[SerializeField]
	Dictionary<PlusSensor,PlusStyle> plusSensors = new Dictionary<PlusSensor,PlusStyle>();

	[SerializeField][ReadOnly]
	ParticleSystem moveFX;

	public ParticleSystem MoveFX
	{
		get
		{
			return moveFX;
		}
	}

	public Dictionary<PlusSensor,PlusStyle> PlusSensors
	{
		get
		{
			return plusSensors;
		}
	}

	[SerializeField]
	LayerMask plusSensorLayerMask;

	[SerializeField][ReadOnly]
	PlayerFlowController playerFlowController;

	InputReceiver inputReceiver;

	public InputReceiver InputReceiver
	{
		get
		{
			return inputReceiver;
		}
	}

	public PlayerSetting PlayerSetting
	{
		get
		{
			return GameController.Instance.PlayerSetting;
		}
	}

	public HpController HpController;

	protected override void Awake ()
	{
		base.Awake ();

		targetRot = m_Transform.rotation;

		rotTime = GameController.Instance.PlayerSetting.RotTime;

		plusRoot = new GameObject ("[PlusRoot]").transform;
		plusRoot.SetParent (m_Transform);
		plusRoot.localPosition = Vector3.zero;

		inputReceiver = InputReceiverFactory.Get (playerStyle);

		playerFlowController = new PlayerFlowController ();
		playerFlowController.Init (this);

		List<PlusSensor> getPlusSensors = new List<PlusSensor> (m_Go.GetComponentsInChildren<PlusSensor> ());

		plusSensors = new Dictionary<PlusSensor, PlusStyle> ();

		getPlusSensors.ForEach (plusSensor=>
			{
				plusSensor.Owner = this;
				plusSensors.Add(plusSensor,PlusStyle.none);
			});

		cacheCount = 0;
		cacheSpeedCount = 0;

		plusStyles = new List<PlusStyle> ();

		currentMoveSpeed = PlayerSetting.MoveSpeed;

		moveFX = this.GetComponentInChildren<ParticleSystem> ();

		moveFX.Stop ();

		PlayerSetting playerSetting = GameController.Instance.PlayerSetting;

		atk = playerSetting.BaseAtk;
		plusAtk = playerSetting.PlusAtk;
		injuredProtectedTime = playerSetting.InjuredProtectedTime;
		hp = playerSetting.Hp;
		plusHp = playerSetting.PlusHp;
		enableInjuredTime = 0f;
	}

	[SerializeField][ReadOnly]
	int atk;

	[SerializeField][ReadOnly]
	int plusAtk;

	[SerializeField][ReadOnly]
	int plusHp;

	[SerializeField][ReadOnly]
	float injuredProtectedTime;

	[SerializeField][ReadOnly]
	int hp;

	[SerializeField][ReadOnly]
	int mostHp;

	public void SyncMostHp ()
	{
		mostHp = hp;
	}

	public int Hp
	{
		get
		{
			return hp;
		}
	}

	[SerializeField][ReadOnly]
	float enableInjuredTime;

	[SerializeField][Range(0,5)]
	int playerIndex;

	public int PlayerIndex
	{
		get
		{
			return playerIndex;
		}
	}

	public void SearchInjured ()
	{
		List<PlayerController> otherInjuredControllers = new List<PlayerController> ();
		
		plusSensors.ForEach ((plusSensor, plusStyle)=>
			{
				//攻擊用感測器
				if(plusStyle == PlusStyle.atk)
				{
					otherInjuredControllers = plusSensor.GetOtherController(plusSensorLayerMask.value,this);
				}
			});

		otherInjuredControllers.ForEach (otherInjuredController=>
			{
				otherInjuredController.Injured(atk);
			});
	}

	public void Injured(int atk)
	{
		if (Time.time > enableInjuredTime)
		{
			hp -= atk;
			enableInjuredTime = Time.time + injuredProtectedTime;

			if (hp <= 0) 
			{
				hp = 0;
				this.enabled = false;
			}

			float hpProportion = hp / mostHp;

			HpController.SetValue (hpProportion);
		}
	}

	public Dictionary<Collider,PlusSensor> GetPlusPairs ()
	{
		Dictionary<Collider,PlusSensor> pair = new Dictionary<Collider,PlusSensor> ();

		plusSensors.ForEach ((plusSensor,plusStyle)=>
			{
				List<Collider> colls = plusSensor.GetCollider(plusSensorLayerMask.value,this);

				colls.ForEach((coll)=>
					{
						pair.Add(coll, plusSensor);
					});
			});

		return pair;
	}

	public void GetPlus(PlusStyle plusStyle, Vector3 attachProxyPos)
	{
		plusStyles.Add (plusStyle);

		switch(plusStyle)
		{
		case PlusStyle.speed:
			{
				cacheSpeedCount++;
				FlushSpeed ();
				break;
			}

		case PlusStyle.atk:
			{
				atk += plusAtk;
				break;
			}

		case PlusStyle.hp:
			{
				hp += plusHp;
				break;
			}
		}

		PlusCacheCount ();

		GameObject plusFxPrefab = PlayerSetting.GetFX (plusStyle);
		GameObject plusFxGo = MonoBehaviour.Instantiate (plusFxPrefab);
		plusFxGo.transform.position = attachProxyPos;
	}

	List<PlusStyle> plusStyles = new List<PlusStyle> ();

	float rotTime;
	float rotFinishTime;

	void Update()
	{
		float deltaTime = Time.deltaTime;
		playerFlowController.Stay (deltaTime);

		float currentTime = Time.time;

		if (m_Transform.rotation != targetRot) 
		{
			if (currentTime >= rotFinishTime) 
			{
				m_Transform.rotation = targetRot;
			}
			else
			{
				float rotProgress = (rotFinishTime - currentTime) / rotTime;

				m_Transform.rotation = Quaternion.Lerp (m_Transform.rotation, targetRot, rotProgress);
			}
		}
	}

	public void MoveBreak ()
	{
		m_Rigidbody.velocity = Vector3.zero;
	}

	public void SetVelocity (Vector3 velocity)
	{
		m_Rigidbody.velocity = velocity;
	}

	[SerializeField]
	GameObject other;

	[SerializeField][ReadOnly]
	int cacheCount;

	[SerializeField][ReadOnly]
	int cacheSpeedCount;

	public void PlusCacheCount ()
	{
		cacheCount++;
		FlushSpeed ();
	}

	void FlushSpeed()
	{
		float reduceScale = cacheCount * PlayerSetting.ReduceSpeedScale;

		float plusScale = cacheSpeedCount * PlayerSetting.ReduceSpeedScale;

		float keepSpeedScale = PlayerSetting.KeepSpeedScale;

		float processSpeedScale = (1 - reduceScale) + plusScale;

		if (processSpeedScale < keepSpeedScale) 
		{
			processSpeedScale = keepSpeedScale;
		}

		currentMoveSpeed = PlayerSetting.MoveSpeed * processSpeedScale;
	}

	public void TransferColl (BoxCollider coll)
	{
		BoxCollider newColl = m_Go.AddComponent<BoxCollider> ();

		Transform collTransform = coll.transform;

		Vector3 deltaPos = collTransform.position - m_Transform.position;

		Vector3 center = deltaPos + Tool.MultiV3 (coll.center, collTransform.lossyScale);

		Vector3 size = Tool.MultiV3 (collTransform.lossyScale, coll.size);

		newColl.center = center;
		newColl.size = size;

		Destroy (coll);
	}

	[SerializeField][ReadOnly]
	Quaternion targetRot;

	public void SetTargetRot (float degree)
	{
		Quaternion newRot = Quaternion.Euler (0, degree, 0);

		targetRot = newRot;

		rotFinishTime = Time.time - rotTime;
	}

	[SerializeField][ReadOnly]
	float currentMoveSpeed;

	public float  CurrentMoveSpeed
	{
		get
		{
			return currentMoveSpeed;
		}
	}
}
