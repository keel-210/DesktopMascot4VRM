using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectRotator : MonoBehaviour
{

	RectTransform tra;
	Vector3 EndPos;
	float DelayTime, ChangingTime;
	float Timer;
	Vector3 Speed;
	public void Init (Vector3 start, Vector3 end, float delay, float changing)
	{
		DelayTime = delay;
		ChangingTime = changing;
		Speed = (end - start)/ ChangingTime;
		tra = GetComponent<RectTransform> ();
		EndPos = end;
		tra.rotation = Quaternion.Euler (start);
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
			tra.rotation *= Quaternion.Euler (Speed * Time.deltaTime);
		}
		else
		{
			tra.rotation = Quaternion.Euler (EndPos);
			Destroy (this);
		}
	}
	void OnDisable ()
	{
		Destroy (this);
	}
}