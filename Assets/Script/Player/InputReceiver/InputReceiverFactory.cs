using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputReceiverFactory
{
	public static InputReceiver Get(PlayerStyle playerStyle)
	{
		switch(playerStyle)
		{
			case  PlayerStyle.LeftPlayer:
			{
				return new LeftPlayerInputReceiver ();
			}

			case  PlayerStyle.RightPlayer:
			{
				return new RightPlayerInputReceiver ();
			}

			case  PlayerStyle.MidPlayer:
			{
				return new MidPlayerInputReceiver ();
			}
		}

		throw new Exception ("找不到對應的硬體輸入器");
	}
}
