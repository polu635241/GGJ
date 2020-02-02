using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputReceiver
{
	public abstract bool Right ();

	public abstract bool Left ();

	public abstract bool Up ();

	public abstract bool Down ();

	public abstract bool CatchPlus ();
}
