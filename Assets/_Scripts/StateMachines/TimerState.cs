using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerState : StateMachineBehaviour
{
	[SerializeField] AudioClip sound;
	[SerializeField] public float time;
	float Timer;
	GameObject ForSound;
	bool IsExecuted;
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Timer = 0;
		IsExecuted = false;
		ForSound = new GameObject();
		ForSound.AddComponent<AudioSource>();
	}
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Timer += Time.deltaTime;
		if (Timer > time && !IsExecuted)
		{
			Play();
			IsExecuted = true;
		}
	}
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Destroy(ForSound);
	}
	void Play()
	{
		AudioSource source = ForSound.GetComponent<AudioSource>();
		source.clip = sound;
		source.Play();
	}
}