using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCtrlTest : MonoBehaviour
{
	[SerializeField] public Animator anim;
	void Update ()
	{
		if (anim)
		{
			anim.SetBool ("Next", true);
			Debug.Log ("Anim Check" + anim.GetBool ("Next"));
		}
	}
}