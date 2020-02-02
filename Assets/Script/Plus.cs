using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plus : MonoBehaviour
{
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
