using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GenericEntityController {

	[SerializeField]
	PlayerStyle playerStyle;

	[SerializeField][ReadOnly]
	PlayerSetting playerSetting;

	[SerializeField]
	List<PlusSensor> plusSensor = new List<PlusSensor> ();

	[SerializeField]
	LayerMask plusLayerMask;

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

		plusSensor = new List<PlusSensor> (m_Go.GetComponentsInChildren<PlusSensor> ());
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

	[SerializeField]
	GameObject other;

	[ContextMenu("EatOther")]
	void EatOther()
	{
		BoxCollider otherColl = other.GetComponent<BoxCollider> ();
		BoxCollider newCollider = m_Go.AddComponent<BoxCollider> ();
		newCollider.center = otherColl.center + other.transform.position - m_Transform.position;
		newCollider.size = otherColl.size;

		Destroy (otherColl);

//		newCollider.enabled = false;
//		newCollider.enabled = true;
	}

	[ContextMenu("GetInfo")]
	void GetInfo ()
	{
		Vector3 center = m_Transform.position;
		Vector3 halfExtents = Tool.MultiV3 (m_Collider.size / 2, m_Transform.localScale);

		Collider[] colls = Physics.OverlapBox (center, halfExtents, m_Transform.rotation);

		Array.ForEach (colls, coll=>
			{
				print (coll.gameObject.name);
			});
	}

	void OnCollisionEnter(Collision coll)
	{
//		string enterName = coll.gameObject.name;
//
//		UnityEngine.Debug.LogError(enterName);
	}
}
