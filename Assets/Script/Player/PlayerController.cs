using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GenericEntityController {

	[SerializeField]
	PlayerStyle playerStyle;

	[SerializeField][ReadOnly]
	PlayerSetting playerSetting;

	[SerializeField]
	List<PlusSensor> plusSensors = new List<PlusSensor> ();

	public List<PlusSensor> PlusSensors
	{
		get
		{
			return plusSensors;
		}
	}

	[SerializeField]
	LayerMask plusLayerMask;

	public PlayerSetting PlayerSetting
	{
		get
		{
			return playerSetting;
		}
	}

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

	protected override void Awake ()
	{
		base.Awake ();

		inputReceiver = InputReceiverFactory.Get (playerStyle);

		playerFlowController = new PlayerFlowController ();
		playerFlowController.Init (this);

		plusSensors = new List<PlusSensor> (m_Go.GetComponentsInChildren<PlusSensor> ());

		cacheCount = 0;

		currentMoveSpeed = playerSetting.MoveSpeed;
	}

	public Dictionary<PlusSensor,Collider> GetPlusPairs ()
	{
		Dictionary<PlusSensor, Collider> pair = new Dictionary<PlusSensor, Collider> ();

		plusSensors.ForEach (plusSensor=>
			{
				Collider[] colls = plusSensor.GetCollider(plusLayerMask.value);

				Array.ForEach(colls, (coll)=>
					{
						pair.Add(plusSensor,coll);
					});
			});

		return pair;
	}

	void Update()
	{
		float deltaTime = Time.deltaTime;
		playerFlowController.Stay (deltaTime);
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

	public void PlusCacheCount ()
	{
		cacheCount++;

		float reduceScale = cacheCount * playerSetting.ReduceSpeedScale;

		float keepSpeedScale = PlayerSetting.KeepSpeedScale;

		float processSpeedScale = (1 - reduceScale);

		if (processSpeedScale < keepSpeedScale) 
		{
			processSpeedScale = keepSpeedScale;
		}

		currentMoveSpeed = playerSetting.MoveSpeed * processSpeedScale;
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
	float currentMoveSpeed;

	public float  CurrentMoveSpeed
	{
		get
		{
			return currentMoveSpeed;
		}
	}
}
