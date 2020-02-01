using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class GameController : MonoBehaviour 
{
	public static GameController Instance;

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
