using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBoneObserver : MonoBehaviour
{
	public Animator anim;
	void Start()
	{
		FindObjectOfType<VRMAnimLoader>().NewModelLoadedAnim += (animator) =>
		{
			anim = animator;
		};
	}
	void Update()
	{
		if (anim)
		{
			if (Input.GetAxis("MouseClick") > 0)
			{
				int boneNum = (int)BoneCheck(Input.mousePosition);
				anim.SetInteger("ClickEvent", boneNum);
			}
			else
			{
				anim.SetInteger("ClickEvent", (int)HumanBodyBones.LastBone);
			}
		}
	}
	HumanBodyBones BoneCheck(Vector3 mousePosition)
	{
		HumanBodyBones targetBone = HumanBodyBones.LastBone;
		Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(mouseRay, out hit))
		{
			targetBone = hit.transform.GetComponent<HumanCollider>().HitBone(hit.collider.transform);
		}
		return targetBone;
	}
}