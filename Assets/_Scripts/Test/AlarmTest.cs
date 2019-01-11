using System;
using UnityEngine;

public class AlarmTest : MonoBehaviour
{
	public SerializableDateTime time1;
	public SerializableDateTime time2;
	public AudioClip Audio;
	public int AnimNum;
	void Start()
	{
		Debug.Log(MinuteCompare(time1.Value, time2.Value));
	}
	bool MinuteCompare(DateTime t1, DateTime t2)
	{
		return t1.Hour == t2.Hour && t1.Minute == t2.Minute;
	}
}