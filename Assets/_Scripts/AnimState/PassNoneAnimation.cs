using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassNoneAnimation : StateMachineBehaviour
{
	override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (stateInfo.length == 0)
		{

		}
	}
	override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}
	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}
}