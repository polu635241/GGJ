using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public abstract class PlayerFlowState
{
	protected PlayerFlowController flowController;
	protected int backgroundSensingLayerMask;

	protected PlayerController PlayerController
	{
		get
		{
			return flowController.PlayerController;
		}
	}

	protected InputReceiver InputReceiver
	{
		get
		{
			return PlayerController.InputReceiver;
		}
	}

	protected PlayerSetting PlayerSetting
	{
		get
		{
			return PlayerController.PlayerSetting;
		}
	}

	protected GameController GameController
	{
		get
		{
			return GameController.Instance;
		}
	}


	public PlayerFlowState (PlayerFlowController playerFlowController)
	{
		this.flowController = playerFlowController;
	}
	
	public virtual void Enter(PlayerFlowState prevState)
	{
//		PlayerController.m_Anim.Play (BindAnimationName);
	}
	
	public virtual PlayerFlowState Stay (float deltaTime)
	{
		return null;
	}
	
	public virtual void Exit()
	{
		
	}
	
	protected abstract string BindAnimationName{ get;}
	
	protected PlayerFlowState GetState<T> () where T:PlayerFlowState
	{
		return flowController.GetState<T> ();
	}


	/// <summary>
	/// 沒按下任何方向鍵 回傳null
	/// </summary>
	/// <returns>The input dir.</returns>
	protected int? GetInputDir()
	{
		bool right = InputReceiver.Right ();
		bool left = InputReceiver.Left ();
		bool up = InputReceiver.Up ();
		bool down = InputReceiver.Down ();

		if (right) 
		{
			if (up) 
			{
				//右上
				return 45;
			}
			else if(down)
			{
				//右下
				return 135;
			}
			else
			{
				//右
				return 90;
			}
		}
		else if(left)
		{
			if (up) 
			{
				//左上
				return 315;
			}
			else if(down)
			{
				//左下
				return 225;
			}
			else
			{
				//左
				return 270;
			}
		}
		else
		{
			if (up) 
			{
				//上
				return 0;
			}
			else if(down)
			{
				//下
				return 180;
			}
			else
			{
				//沒按下任何方向鍵
				return null;
			}
		}
	}

	protected void CachePlus ()
	{
		Dictionary<Collider,PlusSensor> plusPairs = PlayerController.GetPlusPairs ();

		Dictionary<Collider,PlusSensor> needProcessPairs = new Dictionary<Collider,PlusSensor> ();

		List<PlusSensor> needRemovePlusSensors = new List<PlusSensor> ();

		plusPairs.ForEach ((BoxCollider, plusSensor)=>
			{
				//同一個物件被多個感應器碰到擇一即可
				if(!needProcessPairs.ContainsKey(BoxCollider))
				{
					BoxCollider.gameObject.layer = LayerMask.NameToLayer(Layers.AttachPlus);
					
					needProcessPairs.Add(BoxCollider,plusSensor);

					if(!needRemovePlusSensors.Contains(plusSensor))
					{
						needRemovePlusSensors.Add(plusSensor);
					}
				}
			});
		
		Timing.RunCoroutine(CatchPlus (needProcessPairs));

//		needRemovePlusSensors.ForEach (plusSensor=>
//			{
//				bool removePlusSensorSuccess= PlayerController.PlusSensors.Remove (plusSensor);
//				
//				if(!removePlusSensorSuccess)
//				{
//					Debug.LogError ("remove fail");
//				}
//			});

	}

	IEnumerator<float> CatchPlus(Dictionary<Collider,PlusSensor> needProcessPairs)
	{
		float beginTime = Time.time;
		float needTime = PlayerSetting.CachePlusTime;
		float remainingTime = needTime;
		float finishTime = beginTime + needTime;

		while (remainingTime > 0 && GameController.GameFlow == GameFlow.CatchPlus)
		{
			float progress = (1 - remainingTime / needTime);

			LerpPlusEntityPos (needProcessPairs, progress);
			yield return Timing.WaitForOneFrame;
			remainingTime = finishTime - Time.time;
		}

		EatFinish (needProcessPairs);
	}

	void EatFinish(Dictionary<Collider,PlusSensor> needProcessPairs)
	{
		needProcessPairs.ForEach ((eatTargetCollider,originPlusSensor)=>
			{
				PlusSensor triggerPlusSensor = eatTargetCollider.GetComponent<PlusSensor>();

				Plus plus = triggerPlusSensor.Plus;

				PlusSensor[] bindPlusSensors =  plus.GetComponentsInChildren<PlusSensor>();

				List<PlusSensor> targetOtherPlusSensors = new List<PlusSensor>(bindPlusSensors);

				bool success = targetOtherPlusSensors.Remove(triggerPlusSensor);

				if(!success)
				{
					Debug.LogError ("not success");
				}

				targetOtherPlusSensors.ForEach(otherSensor=>
					{
						PlayerController.PlusSensors.Add(otherSensor,plus.PlusStyle);
					});


				Transform plusTransform = plus.transform;
				plusTransform.SetParent (PlayerController.PlusRoot);

				Vector3 proxyPos = originPlusSensor.Proxy.position;

				plusTransform.position = proxyPos;

				BoxCollider eatTargetBoxCollider = (BoxCollider)eatTargetCollider;
				PlayerController.TransferColl(eatTargetBoxCollider);
				PlayerController.GetPlus(plus.PlusStyle, proxyPos);
			});

	}

	void LerpPlusEntityPos(Dictionary<Collider,PlusSensor> needProcessPairs, float _value)
	{
		needProcessPairs.ForEach ((eatTargetCollider,originPlusSensor)=>
			{
				Transform eatTargetTransform = eatTargetCollider.transform;

				Plus plus = eatTargetTransform.GetComponent<PlusSensor>().Plus;

				plus.transform.position = Vector3.Lerp(plus.transform.position , originPlusSensor.Proxy.position,_value);
			});
	}
}