using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimatorWriter2State : MonoBehaviour
{
	Animator anim;
	AnimatorConnector[] connecters;
	void Start ()
	{
		List<Animator> Animators = FindObjectsOfType<Animator> ().ToList ();
		anim = GetComponent<Animator> ();
		connecters = anim.GetBehaviours<AnimatorConnector> ();
		foreach (AnimatorConnector c in connecters)
		{
			foreach (Animator a in Animators)
			{
				if (a.runtimeAnimatorController.name.Contains (c.anim.runtimeAnimatorController.name))
				{
					c.anim = a;
				}
			}
		}
	}
}