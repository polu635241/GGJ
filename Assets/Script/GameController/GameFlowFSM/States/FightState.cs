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
			return GetState<KOState> ();
		}
		else
		{
			SetTime (fightFlowTime - eslapedTime);
		}

		bool notAlive = true;

		for (int i = 0; i < flowController.playerControllers.Count; i++) 
		{
			PlayerController playerController = flowController.playerControllers [i];

			playerController.SearchInjured();

			if(playerController.Hp > 0)
			{
				notAlive = false;
			}

			flowController.hpEntitys [i].position = playerController.transform.position;
		}

		if (notAlive) 
		{
			return GetState<KOState> ();
		}

		return null;
	}

	float fightFlowTime;
	float eslapedTime;

	public override void Exit ()
	{
		base.Exit ();

		flowController.playerControllers.ForEach (playerController=>
			{
				playerController.enabled = false;
			});
	}

	protected override GameFlow BindGameFlow 
	{
		get 
		{
			return GameFlow.Fight;
		}
	}
}