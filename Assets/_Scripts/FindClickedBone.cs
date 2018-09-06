using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClickedBone
{
	public Animator animator;
	public bool ClickedBone (Transform tra, out HumanBodyBones bone)
	{
		bone = HumanBodyBones.LastBone;
		foreach (HumanBodyBones b in Enum.GetValues (typeof (HumanBodyBones)))
		{
			if (tra == animator.GetBoneTransform (b))
			{
				bone = b;
				return true;
			}
		}
		return false;
	}
}