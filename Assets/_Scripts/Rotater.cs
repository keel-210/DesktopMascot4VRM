using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
	[SerializeField] float speed;
	void Update ()
	{
		transform.rotation *= Quaternion.Euler (Vector3.one * speed);
	}
}