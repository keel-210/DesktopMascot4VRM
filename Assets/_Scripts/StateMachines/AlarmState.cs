using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmState : StateMachineBehaviour
{
	[SerializeField] SerializableDateTime time;
	[SerializeField] AudioClip sound;
	[SerializeField] int AnimNum;
	[SerializeField] DateTimeCompareType type;
	void Awake()
	{
		FindObjectOfType<AlarmCaller>().AddAlarm(time.Value, sound, AnimNum, type);
	}
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}
}