using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plus : MonoBehaviour
{
	void Awake()
	{
		PlusSensor[] plusSensors= this.GetComponentsInChildren<PlusSensor> ();

		foreach (PlusSensor plusSensor in plusSensors) 
		{
			plusSensor.SetOwner (this);
		}
	}
	
	[SerializeField]
	PlusStyle plusStyle;

	public PlusStyle PlusStyle
	{
		get
		{
			return plusStyle;
		}
	}
}
