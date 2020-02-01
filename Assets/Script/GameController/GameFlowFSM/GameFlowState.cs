using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public abstract class GameFlowState
{
	protected GameFlowController flowController;

	protected GameController GameController
	{
		get
		{
			return GameController.Instance;
		}
	}


	public GameFlowState (GameFlowController gameFlowController)
	{
		this.flowController = gameFlowController;
	}
	
	public virtual void Enter(GameFlowState prevState)
	{
		GameController.GameFlow = BindGameFlow;
	}
	
	public virtual GameFlowState Stay (float deltaTime)
	{
		return null;
	}
	
	public virtual void Exit()
	{
		
	}
	
	protected GameFlowState GetState<T> () where T:GameFlowState
	{
		return flowController.GetState<T> ();
	}

	protected abstract GameFlow BindGameFlow
	{
		get;
	}
}