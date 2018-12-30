using System;
using UnityEngine;

public class TimerTest : MonoBehaviour
{
	[SerializeField] AudioClip clip;
	[SerializeField] float time;
	[SerializeField] int AlarmState;
	void Start()
	{
		FindObjectOfType<TimerCaller>().SetTimer(clip, time, AlarmState);
	}
}