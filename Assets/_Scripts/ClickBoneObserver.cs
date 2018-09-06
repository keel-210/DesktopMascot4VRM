using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBoneObserver : MonoBehaviour
{
	public Animator anim;

	void Update ()
	{
		if (anim)
		{
			if (Input.GetAxis ("MouseClick")> 0)
			{
				int boneNum = (int)BoneCheck (Input.mousePosition);
				Debug.Log (boneNum);
				anim.SetInteger ("ClickEvent", boneNum);
			}
			else
			{
				anim.SetInteger ("ClickEvent", (int)HumanBodyBones.LastBone);
			}
		}

	}
	HumanBodyBones BoneCheck (Vector3 mousePosition)
	{
		HumanBodyBones nearestBone = HumanBodyBones.LastBone;
		float nearestDistance = 100f;
		Debug.Log (Enum.GetValues (typeof (HumanBodyBones)).Length);
		foreach (HumanBodyBones bone in Enum.GetValues (typeof (HumanBodyBones)))
		{
			if ((int)bone < (int)HumanBodyBones.LastBone)
			{
				Transform boneObj = anim.GetBoneTransform (bone);
				if (boneObj)
				{
					Vector3 mousePosOnRay = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					Vector3 mouseDirection = Camera.main.ScreenPointToRay (mousePosition).direction;

					Vector3 boneDirection = boneObj.forward;
					Vector3 Line2LineVec = mousePosOnRay - anim.GetBoneTransform (bone).position;

					Vector3 VertOf2Line = Vector3.Cross (mouseDirection, boneDirection).normalized;

					float distance = Mathf.Abs (Vector3.Dot (Line2LineVec, VertOf2Line));
					if (distance < nearestDistance)
					{
						nearestDistance = distance;
						nearestBone = bone;
					}
				}
			}
		}
		return nearestBone;
	}
}