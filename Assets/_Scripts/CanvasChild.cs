using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasChild : MonoBehaviour
{

	void Start ()
	{
		transform.parent = GameObject.Find ("Canvas").transform;
		transform.localScale = Vector3.one;
	}
}