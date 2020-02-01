using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandbyState : GameFlowState 
{
	public StandbyState (GameFlowController gameFlowController) : base (gameFlowController)
	{

	}
		
	bool intoInputProted;
	float eslapedTime;

	const float ProtectedTime = 1f;

	public override void Enter (GameFlowState prevState)
	{
		base.Enter (prevState);
		intoInputProted = true;
	}

	public override GameFlowState Stay (float deltaTime)
	{
		if (intoInputProted) 
		{
			eslapedTime += deltaTime;

			if (eslapedTime > ProtectedTime) 
			{
				intoInputProted = false;
			}
		}
		
		if (Input.anyKeyDown && !intoInputProted)
		{
			return GetState<LoadState> ();
		}
		
		return null;
	}

	public override void Exit ()
	{
		base.Exit ();
	}

	protected override GameFlow BindGameFlow 
	{
		get 
		{
			return GameFlow.Standby;
		}
	}
}