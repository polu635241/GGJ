using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerFlowState
{
	protected PlayerFlowController flowController;
	protected int backgroundSensingLayerMask;
	
	public PlayerFlowState (PlayerFlowController playerFlowController)
	{
		this.flowController = playerFlowController;
	}
	
	public virtual void Enter(PlayerFlowState prevState)
	{
		
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
}