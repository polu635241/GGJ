using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandbyState : GameFlowState 
{
	public StandbyState (GameFlowController gameFlowController) : base (gameFlowController)
	{

	}

	public override void Enter (GameFlowState prevState)
	{
		base.Enter (prevState);
	}

	public override GameFlowState Stay (float deltaTime)
	{
		return null;
	}

	public override void Exit ()
	{
		base.Exit ();
	}
}