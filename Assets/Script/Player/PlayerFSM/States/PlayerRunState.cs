using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerFlowState {

	public PlayerRunState (PlayerFlowController playerFlowController) : base (playerFlowController)
	{

	}

	public override void Enter (PlayerFlowState prevState)
	{
		base.Enter (prevState);
	}

	public override PlayerFlowState Stay (float deltaTime)
	{
		return null;
	}

	protected override string BindAnimationName 
	{
		get 
		{
			return AnimationNames.Run;
		}
	}
}