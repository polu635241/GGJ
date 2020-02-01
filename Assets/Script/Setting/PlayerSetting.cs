﻿using System.Collections;
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
}
