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
		FindObjectOfType<VRMAnimLoader>().NewModelLoadedAnim += (anim) =>
		{
			animator = anim;
		};
	}
	public void SetTimer(AudioClip clip, float time, int state)
	{
		StartCoroutine(this.DelayMethod(time, () =>
		{
			StartCoroutine(CallTimer(clip, state));
		}));
	}
	IEnumerator CallTimer(AudioClip clip, int state)
	{
		GameObject ForSound = new GameObject();
		ForSound.AddComponent<AudioSource>();
		AudioSource source = ForSound.GetComponent<AudioSource>();
		source.clip = clip;
		animator.SetInteger("AlarmState", state);
		source.Play();
		yield return new WaitForSeconds(source.clip.length);
		Destroy(ForSound);
	}
}