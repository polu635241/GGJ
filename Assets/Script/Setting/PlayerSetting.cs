using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Setting/PlayerSetting")]
public class PlayerSetting : ScriptableObject 
{
	[SerializeField]
	float moveSpeed;

	public float MoveSpeed
	{
		get
		{
			return moveSpeed;
		}
	}

	[SerializeField][Header("物件綁定的時間")]
	float cachePlusTime;

	public float CachePlusTime
	{
		get
		{
			return cachePlusTime;
		}
	}

	[SerializeField][Header("抓東西流程時間")]
	float cachePlusFlowTime;

	public float CachePlusFlowTime
	{
		get
		{
			return cachePlusFlowTime;
		}
	}

	[SerializeField][Header("戰鬥流程時間")]
	float fightFlowTime;

	public float FightFlowTime
	{
		get
		{
			return fightFlowTime;
		}
	}

	[SerializeField]
	float reduceSpeedScale;

	public float ReduceSpeedScale
	{
		get
		{
			return reduceSpeedScale;
		}
	}

	[SerializeField]
	float plusSpeedScale;

	public float PlusSpeedScale
	{
		get
		{
			return plusSpeedScale;
		}
	}

	[SerializeField]
	float keepSpeedScale;

	public float KeepSpeedScale
	{
		get
		{
			return keepSpeedScale;
		}
	}

	[SerializeField]
	GameObject speedFxPrefab;

	[SerializeField]
	GameObject hpFxPrefab;

	[SerializeField]
	GameObject atkFxPrefab;

	public GameObject GetFX(PlusStyle plusStyle)
	{
		switch(plusStyle)
		{
			case PlusStyle.atk:
			{
				return atkFxPrefab;
			}

			case PlusStyle.hp:
			{
				return hpFxPrefab;
			}

			case PlusStyle.speed:
			{
				return speedFxPrefab;
			}
		}

		string plusStyleStr = plusStyle.ToString ();

		throw new UnityException ("找不到對應的預置物 plusStyle -> " + plusStyleStr);
	}

	public float FightFxTime
	{
		get
		{
			return fightFxTime;
		}
	}

	[SerializeField]
	float fightFxTime;

	public float PlusRootFadeOutTime
	{
		get
		{
			return plusRootFadeOutTime;
		}
	}

	[SerializeField]
	float plusRootFadeOutTime;

	public float PlusRootDeltaY
	{
		get
		{
			return plusRootDeltaY;
		}
	}

	[SerializeField]
	float plusRootDeltaY;

	public float RotTime
	{
		get
		{
			return rotTime;
		}
	}

	[SerializeField]
	float rotTime;

	[SerializeField]
	float atkTime;

	public float AtkTime
	{
		get
		{
			return atkTime;
		}
	}

	[SerializeField]
	float atkVelocity;

	public float AtkVelocity
	{
		get
		{
			return atkVelocity;
		}
	}
}
