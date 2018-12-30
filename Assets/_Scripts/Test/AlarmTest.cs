using System;
using UnityEngine;

public class AlarmTest : MonoBehaviour
{
	public SerializableDateTime time;
	public AudioClip Audio;
	public int AnimNum;
	void Start()
	{
		FindObjectOfType<AlarmCaller>().AddAlarm(time.Value, Audio, AnimNum);
	}
}