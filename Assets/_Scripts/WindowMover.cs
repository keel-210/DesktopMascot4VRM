using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowMover : MonoBehaviour
{
	[SerializeField] Transform Model;
	void Update ()
	{
		float mouse_move_x = Input.GetAxis ("Horizontal");
		float mouse_move_y = Input.GetAxis ("Vertical");
		Vector3 axes = new Vector3 (mouse_move_x, mouse_move_y, 0);
		Model.position += axes * 0.01f;
	}
}