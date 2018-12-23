using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class AlarmCaller : MonoBehaviour
{
	public Animator animator;
	List<DateAndSound> list = new List<DateAndSound>();
	void Start()
	{
		FindObjectOfType<VRMAnimLoader>().NewModelLoadedAnim += (anim) =>
		{
			animator = anim;
		};
	}
	void Update()
	{
		if (list.Count > 0)
		{
			var sounds = list.Where(x => DateTime.Compare(x.time, DateTime.Now) == 0).Select(s => s.sound);
			if (sounds.Count() > 0)
			{
				foreach (var s in sounds)
				{
					Alarm(s);
				}
			}
		}
	}
	public void AddAlarm(DateTime time, AudioClip sound)
	{
		var alarm = new DateAndSound();
		alarm.time = time;
		alarm.sound = sound;
		list.Add(alarm);
	}
	void Alarm(AudioClip sound)
	{
		GameObject obj = new GameObject();
		var source = obj.AddComponent<AudioSource>();
		source.clip = sound;
		source.Play();
	}
	public class DateAndSound
	{
		public DateTime time;
		public AudioClip sound;
	}
}