using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class LoadState : GameFlowState 
{
	public LoadState (GameFlowController gameFlowController) : base (gameFlowController)
	{

	}

	bool loadFinish;

	public override void Enter (GameFlowState prevState)
	{
		base.Enter (prevState);
		loadFinish = false;
		Timing.RunCoroutine (WaitLoadSceneCor());
	}

	public override GameFlowState Stay (float deltaTime)
	{
		if (loadFinish) 
		{
			return GetState<CatchPlusState> ();
		}
		
		return null;
	}

	public override void Exit ()
	{	
		base.Exit ();
	}

	IEnumerator<float> WaitLoadSceneCor()
	{	
		CoroutineHandle loadSceneHandle = Timing.RunCoroutine (GameController.IntoPlaySceneCor ());

		yield return Timing.WaitUntilDone (loadSceneHandle);

		loadFinish = true;
	}

	protected override GameFlow BindGameFlow 
	{
		get 
		{
			return GameFlow.Load;
		}
	}
}