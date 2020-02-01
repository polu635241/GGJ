using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetState : GameFlowState 
{
	public ResetState (GameFlowController gameFlowController) : base (gameFlowController)
	{

	}

	public override void Enter (GameFlowState prevState)
	{
		base.Enter (prevState);

		GameController._Reset ();
	}

	public override GameFlowState Stay (float deltaTime)
	{	
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
			return GameFlow.Reset;
		}
	}
}