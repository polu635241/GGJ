using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerFlowController
{
	PlayerController playerController;

	public PlayerController PlayerController
	{
		get
		{
			return playerController;
		}
	}

	public PlayerFlowController (PlayerController playerController)
	{
		this.playerController = playerController;
		playerFlowRepository = new PlayerFlowRepository (this);
		PlayerFlowState initalState = playerFlowRepository.GetState<PlayerIdleState> ();
		ForceChangeState<PlayerIdleState> ();
	}
	
	PlayerFlowState currentState;
	PlayerFlowRepository playerFlowRepository;
	
	[SerializeField][ReadOnly]
	string currentStateInfo;
	
	public void Stay(float deltaTime)
	{
		PlayerFlowState nextState = currentState.Stay (deltaTime);
		
		if (nextState != null) 
		{
			PlayerFlowState prevState = currentState;
			
			currentState.Exit ();
			currentState = nextState;
			currentState.Enter (prevState);
			
			RefreshCurrentStateInfo ();
		}
	}
	
	public T GetState<T> () where T:PlayerFlowState
	{
		return playerFlowRepository.GetState<T> () as T;
	}
	
	public void ForceChangeState<T> () where T:PlayerFlowState
	{
		PlayerFlowState prevState = currentState;
		
		if (currentState != null) 
		{
			currentState.Exit ();
		}
		
		PlayerFlowState nextState = GetState<T> ();
		nextState.Enter (prevState);
		currentState = nextState;
		
		RefreshCurrentStateInfo ();
	}
	
	void RefreshCurrentStateInfo ()
	{
		currentStateInfo = currentState.GetType ().Name;
	}
}