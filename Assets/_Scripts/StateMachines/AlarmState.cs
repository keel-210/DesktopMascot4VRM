using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmState : StateMachineBehaviour
{
	[SerializeField] public AudioClip sound;
	[SerializeField] public SerializableDateTime time;
	GameObject ForSound;
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		ForSound = new GameObject();
		ForSound.AddComponent<AudioSource>();
		Play();
	}
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}
	void Play()
	{
		AudioSource source = ForSound.GetComponent<AudioSource>();
		source.clip = sound;
		source.Play();
	}
}