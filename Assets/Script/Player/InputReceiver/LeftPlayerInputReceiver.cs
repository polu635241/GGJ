using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPlayerInputReceiver : InputReceiver 
{
	public override bool Right ()
	{
		return Input.GetKey (KeyCode.D);
	}

	public override bool Left ()
	{
		return Input.GetKey (KeyCode.A);
	}

	public override bool Up ()
	{
		return Input.GetKey (KeyCode.W);
	}

	public override bool Down ()
	{
		return Input.GetKey (KeyCode.S);
	}

	public override bool CatchPlus ()
	{
		return Input.GetKeyDown (KeyCode.Q);
	}

	public override bool Atk ()
	{
		return Input.GetKeyDown (KeyCode.E);
	}
}
