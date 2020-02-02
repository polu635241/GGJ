using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkState : PlayerFlowState {

	public PlayerAtkState (PlayerFlowController playerFlowController) : base (playerFlowController)
	{
		atkTime = PlayerSetting.AtkTime;
		settingAtkVelocity = PlayerSetting.AtkVelocity;
	}
		
	float settingAtkVelocity;

	float atkTime;

	float eslapedTime;

	public override void Enter (PlayerFlowState prevState)
	{
		base.Enter (prevState);

		eslapedTime = 0f;
	}

	public override PlayerFlowState Stay (float deltaTime)
	{
		eslapedTime += Time.deltaTime;

		if (eslapedTime > atkTime) 
		{
			return GetState<PlayerIdleState> ();
		}
		else
		{
			float atkProgress = (eslapedTime - atkTime) / atkTime;

			float atkVelocity = Mathf.Lerp (settingAtkVelocity, 0, atkProgress);

			Vector3 processAtkVelocity = PlayerController.m_Transform.forward * atkVelocity;

			PlayerController.SetVelocity (processAtkVelocity);
		}
		

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
			return AnimationNames.Run;
		}
	}
}