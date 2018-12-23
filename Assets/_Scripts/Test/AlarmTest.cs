using System;
using UnityEngine;

public class AlarmTest : MonoBehaviour
{
	public SerializableDateTime time;
	public AudioClip Audio;
	void Start()
	{
		FindObjectOfType<AlarmCaller>().AddAlarm(time.Value, Audio);
	}
}