using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlowRepository
{
	public PlayerFlowRepository(PlayerFlowController playerFlowController)
	{
		InitTable (playerFlowController);
	}
	
	Dictionary<Type,PlayerFlowState> playerFlowStateDictTable;
	
	public PlayerFlowState GetState<T> () where T:PlayerFlowState
	{
		PlayerFlowState state = null;
		
		Type type = typeof(T);
		
		if (playerFlowStateDictTable.TryGetValue (type, out state))
		{
			return state as T;
		}
		else
		{
			throw new UnityException (string.Format ("Can't get state -> {0}", type));
		}
	}
	
	void InitTable(PlayerFlowController playerFlowController)
	{
		playerFlowStateDictTable = new Dictionary<Type, PlayerFlowState> ();
		playerFlowStateDictTable.Add (typeof(PlayerIdleState), new PlayerIdleState (playerFlowController));
		playerFlowStateDictTable.Add (typeof(PlayerRunState), new PlayerRunState (playerFlowController));
		playerFlowStateDictTable.Add (typeof(PlayerJump_RiseState), new PlayerJump_RiseState (playerFlowController));
		playerFlowStateDictTable.Add (typeof(PlayerJump_DownState), new PlayerJump_DownState (playerFlowController));
	}
}