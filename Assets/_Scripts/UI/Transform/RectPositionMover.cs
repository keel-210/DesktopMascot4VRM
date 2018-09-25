using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectPositionMover : MonoBehaviour
{
	RectTransform tra;
	Vector3 StartPos, EndPos;
	float DelayTime, MovingTime;
	float Timer;
	Vector3 Speed;
	public void Init (Vector3 start, Vector3 end, float delay, float moving)
	{
		StartPos = start;
		DelayTime = delay;
		MovingTime = moving;
		Speed = (end - start)/ MovingTime;
		tra = GetComponent<RectTransform> ();
		EndPos = tra.position;
		tra.position += start;
	}
	void Start ()
	{
		tra = GetComponent<RectTransform> ();
	}
	void Update ()
	{
		if (Timer < DelayTime)
		{
			Timer += Time.deltaTime;
		}
		else if (Timer < DelayTime + MovingTime)
		{
			Timer += Time.deltaTime;
			tra.position += Speed * Time.deltaTime;
		}
		else
		{
			Destroy (this);
		}
	}
	void OnDisable ()
	{
		Destroy (this);
	}
}