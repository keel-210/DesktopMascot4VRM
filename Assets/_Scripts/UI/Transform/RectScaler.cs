using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectScaler : MonoBehaviour
{

	RectTransform tra;
	Vector3 StartPos, EndPos;
	float DelayTime, ChangingTime;
	float Timer;
	Vector3 Speed;
	public void Init (Vector3 start, Vector3 end, float delay, float changing)
	{
		StartPos = start;
		DelayTime = delay;
		ChangingTime = changing;
		Speed = (end - StartPos)/ ChangingTime;
		tra = GetComponent<RectTransform> ();
		EndPos = end;
		tra.localScale = start;
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
		else if (Timer < DelayTime + ChangingTime)
		{
			Timer += Time.deltaTime;
			tra.localScale += Speed * Time.deltaTime;
		}
		else
		{
			tra.localScale = EndPos;
			Destroy (this);
		}
	}
	void OnDisable ()
	{
		Destroy (this);
	}
}