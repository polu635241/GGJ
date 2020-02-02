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

	[SerializeField][ReadOnly]
	Plus plus;

	public Plus Plus
	{
		get
		{
			return plus;
		}

		set
		{
			plus = value;
		}
	}

	public bool hasOwner
	{
		get
		{
			return Owner != null;
		}
	}

	public PlayerController Owner = null;

	[SerializeField]
	SensorStyle sensorStyle = SensorStyle.Peace;

	public SensorStyle SensorStyle
	{
		get
		{
			return sensorStyle;
		}
	}
	
	[SerializeField]
	Transform proxy;
	
	public List<Collider> GetCollider (int plusMask, int plusSensorMask, PlayerController owner)
	{
		Vector3 center = m_Transform.position;
		Vector3 halfExtents = Tool.MultiV3 (m_Collider.size / 2, m_Transform.localScale);

		Collider[] plusSensorColls = Physics.OverlapBox (center, halfExtents, m_Transform.rotation, plusSensorMask);

		return ProcessColls (plusSensorColls, owner);
	}

	List<Collider> ProcessColls (Collider[] plusSensorColls, PlayerController owner)
	{
		List<Collider> processColls = new List<Collider> ();
		
		Array.ForEach (plusSensorColls, coll => 
			{
				PlusSensor plusSensor = coll.gameObject.GetComponent<PlusSensor> ();

				// layer擺錯 或者 掃到有主人的了
				if(plusSensor != null&&!plusSensor.hasOwner)
				{
					PlusSensor[] bindPlusSensors = plusSensor.Plus.GetComponentsInChildren<PlusSensor>();

					foreach (var bindPlusSensor in bindPlusSensors) 
					{
						bindPlusSensor.Owner = owner;
					}

					bool checkResult = Check(this.sensorStyle, plusSensor.SensorStyle);

					if(checkResult)
					{
						processColls.Add(coll);
					}
				}
			});

		return processColls;
	}

	bool Check(SensorStyle s1, SensorStyle s2)
	{
		if (s1 == SensorStyle.Positive && s2 == SensorStyle.Negative) 
		{
			return true;
		}
		 
		if(s1 == SensorStyle.Negative && s2 == SensorStyle.Positive)
		{
			return true;
		}

		if(s1 == SensorStyle.Peace && s2 == SensorStyle.Peace)
		{
			return true;
		}

		return false;
	}
}
