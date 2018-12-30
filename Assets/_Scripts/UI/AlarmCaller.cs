using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class AlarmCaller : MonoBehaviour
{
	Animator animator;
	List<Alarm> list = new List<Alarm>();
	void Start()
	{
		FindObjectOfType<VRMAnimLoader>().NewModelLoadedAnim += (anim) =>
		{
			animator = anim;
		};
		StartCoroutine(AlarmCheck());
	}
	IEnumerator AlarmCheck()
	{
		if (list.Count > 0)
		{
			var sounds = list.Where(x => DateTime.Compare(x.time, DateTime.Now) == 0);
			if (sounds.Count() > 0)
			{
				foreach (var s in sounds)
				{
					CallAlarm(s);
				}
			}
		}
		yield return new WaitForSeconds(60f);
	}
	public void AddAlarm(DateTime time, AudioClip sound, int AnimNum)
	{
		var alarm = new Alarm();
		alarm.time = time;
		alarm.sound = sound;
		alarm.AnimNum = AnimNum;
		list.Add(alarm);
	}
	void CallAlarm(Alarm d)
	{
		GameObject obj = new GameObject();
		var source = obj.AddComponent<AudioSource>();
		source.clip = d.sound;
		source.Play();
		animator.SetInteger("AlarmEvent", d.AnimNum);
	}
	public class Alarm
	{
		public DateTime time;
		public AudioClip sound;
		public int AnimNum;
	}
}