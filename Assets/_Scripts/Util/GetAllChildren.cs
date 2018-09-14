using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GetAllChildren
{
	public static List<GameObject> GetAll (this GameObject obj)
	{
		List<GameObject> allChildren = new List<GameObject> ();
		GetChildren (obj, ref allChildren);
		return allChildren;
	}

	//子要素を取得してリストに追加
	public static void GetChildren (GameObject obj, ref List<GameObject> allChildren)
	{
		Transform children = obj.GetComponentInChildren<Transform> ();
		//子要素がいなければ終了
		if (children.childCount == 0)
		{
			return;
		}
		foreach (Transform ob in children)
		{
			allChildren.Add (ob.gameObject);
			GetChildren (ob.gameObject, ref allChildren);
		}
	}
}