using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerFlowState {
	
	public PlayerIdleState (PlayerFlowController playerFlowController) : base (playerFlowController)
	{
		
	}
	
	public override void Enter (PlayerFlowState prevState)
	{
		base.Enter (prevState);

		PlayerController.MoveBreak ();
	}
	
	public override PlayerFlowState Stay (float deltaTime)
	{
		if (InputReceiver.CatchPlus ()) 
		{
			CachePlus ();
		}
		
		if (GetInputDir () != null) 
		{
			return GetState<PlayerRunState> ();
		}

		if (InputReceiver.Atk () && GameController.GameFlow == GameFlow.Fight)
		{
			return GetState<PlayerAtkState> ();
		}

		return null;
	}

	protected override string BindAnimationName 
	{
		get 
		{
			return AnimationNames.Idle;
		}
	}
}