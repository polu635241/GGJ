using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GenericEntityController {

	[SerializeField]
	PlayerStyle playerStyle;

	PlayerFlowController playerFlowController;

	InputReceiver inputReceiver;

	protected override void Awake ()
	{
		base.Awake ();

		inputReceiver = InputReceiverFactory.Get (playerStyle);

		playerFlowController = new PlayerFlowController (this);
	}
}
