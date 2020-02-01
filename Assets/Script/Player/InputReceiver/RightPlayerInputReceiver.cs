using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPlayerInputReceiver : InputReceiver
{
	public override bool Right ()
	{
		return Input.GetKey (KeyCode.Alpha6);
	}

	public override bool Left ()
	{
		return Input.GetKey (KeyCode.Alpha4);
	}

	public override bool Up ()
	{
		return Input.GetKey (KeyCode.Alpha8);
	}

	public override bool Down ()
	{
		return Input.GetKey (KeyCode.Alpha2);
	}

	public override bool CatchPlus ()
	{
		return Input.GetKeyDown (KeyCode.Plus);
	}
}
