using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEntityController : MonoBehaviour {

	protected virtual void Awake()
	{
		GetGenericComponents ();
	}

	GameObject m_go;

	public GameObject m_Go
	{
		get
		{
			return m_go;
		}
	}

	Transform m_transform;

	public Transform m_Transform
	{
		get 
		{ 
			return m_transform;
		}
	}

	Rigidbody m_rigidbody;

	public Rigidbody m_Rigidbody
	{
		get
		{
			return m_rigidbody;
		}
	}

	Collider m_collider;

	public Collider m_Collider
	{
		get
		{
			return m_collider;
		}
	}

	Animator m_anim;

	public Animator m_Anim
	{
		get
		{
			return m_anim;
		}
	}

	void GetGenericComponents()
	{
		m_go = this.gameObject;
		m_transform = m_go.transform;
		m_rigidbody = m_go.GetComponent<Rigidbody> ();
		m_collider = m_go.GetComponent<Collider> ();
		m_anim = m_go.GetComponent<Animator> ();
	}
}
