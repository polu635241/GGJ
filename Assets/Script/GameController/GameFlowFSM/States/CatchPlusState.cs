using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchPlusState : GameFlowState 
{
	public CatchPlusState (GameFlowController gameFlowController) : base (gameFlowController)
	{
		cachePlusFlowTime = GameController.PlayerSetting.CachePlusFlowTime;
	}

	public override void Enter (GameFlowState prevState)
	{
		base.Enter (prevState);

		flowController.fxGO = GameObject.FindWithTag (Tags.FightFx);

		GameObject timeClock = GameObject.FindWithTag (Tags.TimeClockText);

		if (timeClock != null) 
		{
			flowController.clockText = timeClock.GetComponent<Text> ();

			SetTime (cachePlusFlowTime);
		}

		eslapedTime = 0f;

		flowController.camera = GameObject.FindObjectOfType<Camera> ();
		flowController.cameraAnimator = flowController.camera.transform.parent.GetComponent<Animator> ();

		if (flowController.cameraAnimator != null) 
		{
			flowController.cameraAnimator.enabled = false;
		}
	}

	float cachePlusFlowTime;
	float eslapedTime;

	public override GameFlowState Stay (float deltaTime)
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			return GetState<ResetState> ();
		}

		eslapedTime += deltaTime;

		if (eslapedTime > cachePlusFlowTime) 
		{
			SetTime (0f);
			return GetState<WaitFightState> ();
		}
		else
		{
			SetTime (cachePlusFlowTime - eslapedTime);
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
			return GameFlow.CatchPlus;
		}
	}
}