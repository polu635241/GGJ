using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
	[SerializeField]
	Image image;

	[SerializeField][ReadOnly]
	float targetValue;

	[SerializeField][ReadOnly]
	float beginValue;

	[SerializeField]
	float lerpTime;

	[SerializeField]
	float finishLerpTime;

    // Start is called before the first frame update
    void Start()
    {
		targetValue = 1;
		beginValue = 1;
    }

	public void SetValue(float value)
	{
		targetValue = value;
		finishLerpTime = Time.time + lerpTime;
	}

    // Update is called once per frame
    void Update()
    {
		if (Time.time < finishLerpTime) 
		{
			float progress = (finishLerpTime - Time.time) / lerpTime;

			float currentValue = Mathf.Lerp (beginValue, targetValue, progress);

			image.fillAmount = currentValue;
		}
		else
		{
			beginValue = targetValue;
			image.fillAmount = targetValue;
		}
    }
}
