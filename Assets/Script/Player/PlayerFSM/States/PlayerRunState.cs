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
		int? getInputDir = GetInputDir ();

		if (getInputDir != null) 
		{	
			float degree = getInputDir.Value;
			
			float x_Velocity = Mathf.Sin (degree * Mathf.Deg2Rad);
			float z_Velocity = Mathf.Cos (degree * Mathf.Deg2Rad);

			Vector3 moveVelocity = new Vector3 (x_Velocity, 0, z_Velocity).normalized;
			Vector3 processMoveVelocity = moveVelocity * PlayerSetting.MoveSpeed;
			PlayerController.SetVelocity (processMoveVelocity);

			PlayerController.SetRot (degree);
		}
		else
		{
			return GetState<PlayerIdleState> ();
		}
		
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