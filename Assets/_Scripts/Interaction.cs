using System;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
	public TouchAction t;
}

[Serializable]
public class TouchAction
{
	[HideInInspector]
	public HumanBodyBones bone;
	public Quate quate;
	public Mortion mortion;
}
public class Quate
{
	public string QuateString;
	public AudioSource Voice;
}
public class Mortion
{
	public Animation animation;
	public AnimationCurve windowPos;
	public List<Item> Items;
}
public class Item
{
	public GameObject ItemObj;
	public HumanBodyBones bone;
	public Vector3 Pos, Rot;
}