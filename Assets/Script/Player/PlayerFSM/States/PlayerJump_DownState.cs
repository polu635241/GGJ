using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump_DownState : PlayerFlowState 
{
	public PlayerJump_DownState (PlayerFlowController playerFlowController) : base (playerFlowController)
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

	public override void Exit ()
	{
		base.Exit ();
	}

	protected override string BindAnimationName 
	{
		get 
		{
			return AnimationNames.Jump_Down;
		}
	}
}