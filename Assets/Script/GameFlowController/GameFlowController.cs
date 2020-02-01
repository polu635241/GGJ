﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class GameFlowController : MonoBehaviour 
{
	public static GameFlowController Instance;

	public GameFlow GameFlow
	{
		get
		{
			return gameFlow;
		}

		set
		{
			gameFlow = value;
		}
	}

	[SerializeField]
	GameFlow gameFlow;

	// Use this for initialization
	public void Awake () 
	{
		Instance = this;
		DontDestroyOnLoad (this.gameObject);
	}

	public void Reset()
	{
		Timing.KillCoroutines ();
	}
}
