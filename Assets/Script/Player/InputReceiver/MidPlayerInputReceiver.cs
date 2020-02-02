using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidPlayerInputReceiver : InputReceiver 
{
	public override bool Right ()
	{
		return Input.GetKey (KeyCode.RightArrow);
	}

	public override bool Left ()
	{
		return Input.GetKey (KeyCode.LeftArrow);
	}

	public override bool Up ()
	{
		return Input.GetKey (KeyCode.UpArrow);
	}

	public override bool Down ()
	{
		return Input.GetKey (KeyCode.DownArrow);
	}

	public override bool CatchPlus ()
	{
		return Input.GetKeyDown (KeyCode.RightShift);
	}

	public override bool Atk ()
	{
		return Input.GetKeyDown (KeyCode.Return);
	}
}
