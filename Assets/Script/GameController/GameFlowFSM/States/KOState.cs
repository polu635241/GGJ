using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOState : GameFlowState 
{
	public KOState (GameFlowController gameFlowController) : base (gameFlowController)
	{
		
	}

	float eslapedTime;

	bool hasClosePlusRoot;

	float fightFxTime;

	float plusRootFadeOutTime;

	float plusRootDeltaY;

	Transform plusRootTransform;

	GameObject fxGO;

	Vector3 originPos;

	public override void Enter (GameFlowState prevState)
	{
		base.Enter (prevState);
		flowController.fxGOAnim.Play (Animations.KO);
	}

	public override GameFlowState Stay (float deltaTime)
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			return GetState<ResetState> ();
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
			return GameFlow.KO;
		}
	}
}