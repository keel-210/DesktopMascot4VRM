using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCollider : MonoBehaviour
{
	//VRMの中にcollider入れたくないのとあくまでhumanbodybonesを介しておきたいためこのような設計に
	public List<Transform> colliderList = new List<Transform> ();
	[SerializeField] Animator anim;
	void Start ()
	{
		foreach (Transform t in transform)
		{
			colliderList.Add (t);
		}
		Adjust4Model ();
	}

	void Update ()
	{
		if (anim)
		{
			MoveCollider ();
		}
	}
	public void Adjust4Model ()
	{
		anim = FindObjectOfType<ClickBoneObserver> ().anim;
	}
	void MoveCollider ()
	{
		for (int i = 0; i < targetBones.Length; i++)
		{
			Transform boneObj = anim.GetBoneTransform (targetBones[i]);
			if (boneObj)
			{
				colliderList[i].position = boneObj.position;
				colliderList[i].rotation = boneObj.rotation;
			}
		}
	}
	public HumanBodyBones HitBone (Transform tra)
	{
		int boneNum = colliderList.IndexOf (tra);
		if (boneNum == -1)
		{
			return HumanBodyBones.LastBone;
		}
		return targetBones[boneNum];
	}
	HumanBodyBones[] targetBones = {
		HumanBodyBones.Hips,
		HumanBodyBones.LeftUpperLeg,
		HumanBodyBones.RightUpperLeg,
		HumanBodyBones.LeftLowerLeg,
		HumanBodyBones.RightLowerLeg,
		HumanBodyBones.LeftFoot,
		HumanBodyBones.RightFoot,
		HumanBodyBones.Spine,
		HumanBodyBones.Chest,
		HumanBodyBones.Neck,
		HumanBodyBones.Head,
		HumanBodyBones.LeftShoulder,
		HumanBodyBones.RightShoulder,
		HumanBodyBones.LeftUpperArm,
		HumanBodyBones.RightUpperArm,
		HumanBodyBones.LeftLowerArm,
		HumanBodyBones.RightLowerArm,
		HumanBodyBones.LeftHand,
		HumanBodyBones.RightHand,
		HumanBodyBones.UpperChest
	};
}