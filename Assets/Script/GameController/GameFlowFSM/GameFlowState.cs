using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

public abstract class GameFlowState
{
	protected GameFlowController flowController;

	protected GameController GameController
	{
		get
		{
			return GameController.Instance;
		}
	}

    string TimeTransferMilliSecond(float time, bool ignoreMilliSecond = false)
    {
        float remain = 0;

        string minute = "";
        string second = "";
        string milliSecond = "";

        if (time >= 60)
        {
            minute = ((int)time / 60).ToString("00");
            remain = (time % 60);
        }
        else
        {
            minute = "00";
            remain = time;
        }

        float round = (float)System.Math.Floor(remain);

        second = round.ToString("00");

        string result = "";

        if (ignoreMilliSecond)
        {
            result = string.Format("{0}:{1}", minute, second);
        }
        else
        {
            milliSecond = System.Math.Floor(((remain - round) * 100)).ToString("00");
            result = string.Format("{0}:{1}:{2}", minute, second, milliSecond);
        }

        return result;
    }
    protected void SetTime(float time)
	{
		Text clockText = flowController.clockText;
		if (clockText.text != null) 
		{
            clockText.text = TimeTransferMilliSecond(time);
        }
	}

	public GameFlowState (GameFlowController gameFlowController)
	{
		this.flowController = gameFlowController;
	}
	
	public virtual void Enter(GameFlowState prevState)
	{
		GameController.GameFlow = BindGameFlow;
	}
	
	public virtual GameFlowState Stay (float deltaTime)
	{
		return null;
	}
	
	public virtual void Exit()
	{
		
	}
	
	protected GameFlowState GetState<T> () where T:GameFlowState
	{
		return flowController.GetState<T> ();
	}

	protected abstract GameFlow BindGameFlow
	{
		get;
	}
}