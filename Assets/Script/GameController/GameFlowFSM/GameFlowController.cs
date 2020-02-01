using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GameFlowController
{
	public GameFlowController ()
	{
		
	}

	public void Init()
	{
		gameFlowRepository = new GameFlowRepository (this);
		ForceChangeState<StandbyState> ();
	}
	
	GameFlowState currentState;
	GameFlowRepository gameFlowRepository;
	
	[SerializeField][ReadOnly]
	string currentStateInfo;

	[SerializeField]
	public GameObject fxGO;

	public Text clockText;

	public void Stay(float deltaTime)
	{
		GameFlowState nextState = currentState.Stay (deltaTime);
		
		if (nextState != null) 
		{
			GameFlowState prevState = currentState;
			
			currentState.Exit ();
			currentState = nextState;
			currentState.Enter (prevState);
			
			RefreshCurrentStateInfo ();
		}
	}
	
	public T GetState<T> () where T:GameFlowState
	{
		return gameFlowRepository.GetState<T> () as T;
	}
	
	public void ForceChangeState<T> () where T:GameFlowState
	{
		GameFlowState prevState = currentState;
		
		if (currentState != null) 
		{
			currentState.Exit ();
		}
		
		GameFlowState nextState = GetState<T> ();
		nextState.Enter (prevState);
		currentState = nextState;
		
		RefreshCurrentStateInfo ();
	}
	
	void RefreshCurrentStateInfo ()
	{
		currentStateInfo = currentState.GetType ().Name;
	}
}