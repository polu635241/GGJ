﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitFightState : GameFlowState 
{
	public WaitFightState (GameFlowController gameFlowController) : base (gameFlowController)
	{
		PlayerSetting playerSetting = GameController.PlayerSetting;

		fightFxTime = playerSetting.FightFxTime;
		plusRootFadeOutTime = playerSetting.PlusRootFadeOutTime;
		plusRootDeltaY = playerSetting.PlusRootDeltaY;
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

		hasClosePlusRoot = false;

		GameObject plusRoot = GameObject.FindGameObjectWithTag (Tags.PlusRoot);

		if (plusRoot != null) 
		{
			plusRootTransform = plusRoot.transform;
			originPos = plusRootTransform.position;
		}

		if (flowController.cameraAnimator != null) 
		{
			flowController.cameraAnimator.enabled = true;
		}

		flowController.playerControllers.ForEach (playerController=>
			{
				playerController.HpController.gameObject.SetActive (true);
			});
	}

	public override GameFlowState Stay (float deltaTime)
	{
		eslapedTime += Time.deltaTime;

		if (eslapedTime > plusRootFadeOutTime) 
		{
			if (!hasClosePlusRoot) 
			{
				if (plusRootTransform != null) 
				{
					plusRootTransform.gameObject.SetActive (false);
				}
				
				hasClosePlusRoot = true;

				if (flowController.fxGOAnim != null) 
				{
					flowController.fxGOAnim.Play (Animations.Ready);
				}
			}

			if (eslapedTime > (plusRootFadeOutTime + fightFxTime)) 
			{
				return GetState<FightState> ();
			}
		}
		else
		{
			if (plusRootTransform != null) 
			{
				Vector3 processPos = originPos;

				processPos.y += plusRootDeltaY * (eslapedTime / plusRootFadeOutTime);

				plusRootTransform.position = processPos;
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			return GetState<ResetState> ();
		}

		return null;
	}

	public override void Exit ()
	{
		base.Exit ();

		if (flowController.fxGOAnim != null) 
		{
			flowController.fxGOAnim.Play (Animations.Temp);
		}
	}

	protected override GameFlow BindGameFlow 
	{
		get 
		{
			return GameFlow.WaitFight;
		}
	}
}