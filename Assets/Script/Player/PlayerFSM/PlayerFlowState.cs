using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


	public PlayerFlowState (PlayerFlowController playerFlowController)
	{
		this.flowController = playerFlowController;
	}
	
	public virtual void Enter(PlayerFlowState prevState)
	{
		PlayerController.m_Anim.Play (BindAnimationName);
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
		List<PlusSensor> removePlusSensors = new List<PlusSensor> ();
		
		Dictionary<PlusSensor,Collider> plusPairs = PlayerController.GetPlusPairs ();

		Dictionary<PlusSensor,Collider> needProcessPairs = new Dictionary<PlusSensor, Collider> ();

		plusPairs.ForEach ((plusSensor, BoxCollider)=>
			{
				//同一個物件被多個感應器碰到擇一即可
				if(!needProcessPairs.ContainsValue(BoxCollider))
				{
					needProcessPairs.Add(plusSensor,BoxCollider);

					BoxCollider.gameObject.layer = LayerMask.NameToLayer(Layers.AttachPlus);

					bool removePlusSensorSuccess= PlayerController.PlusSensors.Remove (plusSensor);

					if(!removePlusSensorSuccess)
					{
						Debug.LogError ("remove fail");
					}
				}
			});

		EatFinish (needProcessPairs);
	}

	void EatFinish(Dictionary<PlusSensor,Collider> needProcessPairs)
	{
		needProcessPairs.ForEach ((originPlusSensor,eatTargetCollider)=>
			{
				PlusSensor eatTargetPlusSensor = eatTargetCollider.GetComponentInChildren<PlusSensor> ();
				
				PlayerController.PlusSensors.Add (eatTargetPlusSensor);

				Transform eatTargetTransform = eatTargetCollider.transform;
				eatTargetTransform.SetParent (PlayerController.m_Transform);

				eatTargetTransform.position = originPlusSensor.Proxy.position;

				PlayerController.PlusCacheCount ();

				BoxCollider eatTargetBoxCollider = (BoxCollider)eatTargetCollider;

				PlayerController.TransferColl(eatTargetBoxCollider);
			});
	}
}