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
		EndPos = end;
		DelayTime = delay;
		MovingTime = moving;
		Speed = (EndPos - StartPos)/ MovingTime;
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
			tra.position += Speed * Time.deltaTime;
		}
		else
		{
			tra.position = EndPos;
		}
	}
}