using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowRepository
{
	public GameFlowRepository(GameFlowController gameFlowController)
	{
		InitTable (gameFlowController);
	}
	
	Dictionary<Type,GameFlowState> gameFlowStateDictTable;
	
	public GameFlowState GetState<T> () where T:GameFlowState
	{
		GameFlowState state = null;
		
		Type type = typeof(T);
		
		if (gameFlowStateDictTable.TryGetValue (type, out state))
		{
			return state as T;
		}
		else
		{
			throw new UnityException (string.Format ("Can't get state -> {0}", type));
		}
	}
	
	void InitTable(GameFlowController gameFlowController)
	{
		gameFlowStateDictTable = new Dictionary<Type, GameFlowState> ();
		gameFlowStateDictTable.Add (typeof(StandbyState), new StandbyState (gameFlowController));
		gameFlowStateDictTable.Add (typeof(CatchPlusState), new CatchPlusState (gameFlowController));
		gameFlowStateDictTable.Add (typeof(FightState), new FightState (gameFlowController));
	}
}