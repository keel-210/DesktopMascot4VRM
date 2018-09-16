using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceTest : MonoBehaviour
{
	public List<GameObject> objlist = new List<GameObject> ();
	public List<Vector3> poslist = new List<Vector3> ();

	void Start ()
	{
		for (int i = 0; i < objlist.Count; i++)
		{
			Instantiate (objlist[i], poslist[i], Quaternion.identity);
		}
	}
}