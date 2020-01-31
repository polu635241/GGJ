using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GenericEntityController {

	[SerializeField]
	PlayerStyle playerStyle;

	[SerializeField][ReadOnly]
	PlayerSetting playerSetting;

	public PlayerSetting PlayerSetting
	{
		get
		{
			return playerSetting;
		}
	}

	[SerializeField][ReadOnly]
	PlayerFlowController playerFlowController;

	InputReceiver inputReceiver;

	public InputReceiver InputReceiver
	{
		get
		{
			return inputReceiver;
		}
	}

	protected override void Awake ()
	{
		base.Awake ();

		inputReceiver = InputReceiverFactory.Get (playerStyle);

		playerFlowController = new PlayerFlowController (this);
	}

	void Update()
	{
		float deltaTime = Time.deltaTime;
		playerFlowController.Stay (deltaTime);
	}

	public void MoveBreak ()
	{
		m_Rigidbody.velocity = Vector3.zero;
	}

	public void SetVelocity (Vector3 velocity)
	{
		m_Rigidbody.velocity = velocity;
	}

	public void SetRot (float degree)
	{
		Quaternion newRot = Quaternion.Euler (0, degree, 0);

		m_Transform.rotation = newRot;
	}
}
