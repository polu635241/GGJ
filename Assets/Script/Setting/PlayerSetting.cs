using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Setting/PlayerSetting")]
public class PlayerSetting : ScriptableObject 
{
	[SerializeField]
	float moveSpeed;

	public float MoveSpeed
	{
		get
		{
			return moveSpeed;
		}
	}

	[SerializeField]
	float jumpForce;

	public float JumpForce
	{
		get
		{
			return jumpForce;
		}
	}
}
