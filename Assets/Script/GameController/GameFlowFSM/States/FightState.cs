using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : GameFlowState 
{
	public FightState (GameFlowController gameFlowController) : base (gameFlowController)
	{
		fightFlowTime = GameController.PlayerSetting.FightFlowTime;
	}

	public override void Enter (GameFlowState prevState)
	{
		base.Enter (prevState);
	}

	public override GameFlowState Stay (float deltaTime)
	{	
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			return GetState<ResetState> ();
		}

		eslapedTime += deltaTime;

		if (eslapedTime > fightFlowTime) 
		{
			SetTime (0f);
			return GetState<ResetState> ();
		}
		else
		{
			SetTime (fightFlowTime - eslapedTime);
		}
		
		return null;
	}

	float fightFlowTime;
	float eslapedTime;

	public override void Exit ()
	{
		base.Exit ();
	}

	protected override GameFlow BindGameFlow 
	{
		get 
		{
			return GameFlow.Fight;
		}
	}
}