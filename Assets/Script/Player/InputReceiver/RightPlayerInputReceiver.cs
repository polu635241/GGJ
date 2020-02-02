using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPlayerInputReceiver : InputReceiver
{
	public override bool Right ()
	{
		return Input.GetKey (KeyCode.Keypad6);
	}

	public override bool Left ()
	{
		return Input.GetKey (KeyCode.Keypad4);
	}

	public override bool Up ()
	{
		return Input.GetKey (KeyCode.Keypad8);
	}

	public override bool Down ()
	{
		return Input.GetKey (KeyCode.Keypad2);
	}

	public override bool CatchPlus ()
	{
		return Input.GetKeyDown (KeyCode.Plus);
	}
}
