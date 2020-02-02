using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusSensor : GenericEntityController 
{
	public Transform Proxy
	{
		get
		{
			return proxy;
		}
	}
	
	[SerializeField]
	Transform proxy;
	
	public Collider[] GetCollider (int layerMask)
	{
		Vector3 center = m_Transform.position;
		Vector3 halfExtents = Tool.MultiV3 (m_Collider.size / 2, m_Transform.localScale);

		Collider[] colls = Physics.OverlapBox (center, halfExtents, m_Transform.rotation, layerMask);
		return colls;
	}
}
