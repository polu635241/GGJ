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

	[SerializeField]
	float cachePlusTime;

	public float CachePlusTime
	{
		get
		{
			return cachePlusTime;
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
}
