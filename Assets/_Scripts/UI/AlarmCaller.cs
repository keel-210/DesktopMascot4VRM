using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class AlarmCaller : MonoBehaviour
{
	Animator animator;
	List<Alarm> AlarmList = new List<Alarm>();
	List<Alarm> ExecutedAlarm = new List<Alarm>();

	void Start()
	{
		FindObjectOfType<VRMAnimLoader>().NewModelLoadedAnim += (anim) =>
		{
			animator = anim;
		};
		animator = FindObjectOfType<VRMAnimLoader>().animator;
		StartCoroutine(AlarmCheckCoroutine());
	}
	IEnumerator AlarmCheckCoroutine()
	{
		var alarms = AlarmList.Where(x => DateTimeCompare.Compare(x.time, DateTime.Now, x.type));
		foreach (var a in alarms)
		{
			StartCoroutine(CallAlarm(a));
		}
		var returnAlarms = ExecutedAlarm.Where(x => !DateTimeCompare.Compare(x.time, DateTime.Now, x.type));
		foreach (var r in returnAlarms)
		{
			ExecutedAlarm.Remove(r);
			AlarmList.Add(r);
		}
		yield return new WaitForSeconds(1f);
	}
	public void AddAlarm(DateTime time, AudioClip sound, int AnimNum, DateTimeCompareType type)
	{
		var alarm = new Alarm(time, sound, AnimNum, type);
		AlarmList.Add(alarm);
	}
	IEnumerator CallAlarm(Alarm d)
	{
		GameObject obj = new GameObject();
		var source = obj.AddComponent<AudioSource>();
		source.clip = d.sound;
		source.Play();
		animator.SetInteger("AlarmEvent", d.AnimNum);
		yield return new WaitForSeconds(source.clip.length > animator.GetCurrentAnimatorStateInfo(0).length ? source.clip.length : animator.GetCurrentAnimatorStateInfo(0).length);
		Destroy(obj);
		ExecutedAlarm.Add(d);
		AlarmList.Remove(d);
	}
	public class Alarm
	{
		public Alarm() {}
		public Alarm(DateTime time, AudioClip sound, int AnimNum, DateTimeCompareType type)
		{
			this.time = time;
			this.sound = sound;
			this.AnimNum = AnimNum;
			this.type = type;
		}
		public DateTime time;
		public AudioClip sound;
		public int AnimNum;
		public DateTimeCompareType type;
	}
}