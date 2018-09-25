using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
	[SerializeField] Vector3 speed;
	void Update ()
	{
		transform.rotation *= Quaternion.Euler (speed * Time.deltaTime);
	}
}