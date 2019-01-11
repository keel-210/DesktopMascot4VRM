using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TimerCaller : MonoBehaviour
{
	Animator animator;
	void Start()
	{
		animator = FindObjectOfType<VRMAnimLoader>().animator;
	}
	public void SetTimer(AudioClip clip, float time, int state)
	{
		StartCoroutine(CallTimer(clip, time, state));
	}
	IEnumerator CallTimer(AudioClip clip, float time, int state)
	{
		GameObject Timer = Instantiate((GameObject)Resources.Load("_Prefabs/uGUI/Timer"));
		TimerController ctrl = Timer.GetComponent<TimerController>();
		Timer.AddComponent<AudioSource>();
		AudioSource source = Timer.GetComponent<AudioSource>();
		source.clip = clip;
		if (!animator)
		{
			animator = FindObjectOfType<VRMAnimLoader>().animator;
		}
		animator.SetInteger("AlarmEvent", state);
		float startTime = Time.time;
		while (true)
		{
			ctrl.time = time - Time.time + startTime;
			if (ctrl.time <= 0 || !ctrl) { break; }
			yield return null;
		}
		source.Play();
		yield return new WaitForSeconds(clip.length);
		Destroy(Timer);
	}
}