using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tool  
{
	public static Vector3 MultiV3(Vector3 v3_1, Vector3 v3_2)
	{
		return new Vector3 (v3_1.x * v3_2.x, v3_1.y * v3_2.y, v3_1.z * v3_2.z);
	}

	public static void ForEach<TKey,TValue>(this Dictionary<TKey,TValue> dict, Action<TKey,TValue> callback)
	{
		foreach (KeyValuePair<TKey,TValue> item in dict) 
		{
			callback.Invoke (item.Key, item.Value);
		}
	}
}
