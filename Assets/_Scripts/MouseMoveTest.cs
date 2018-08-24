using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMoveTest : MonoBehaviour
{
	[SerializeField] SpriteRenderer sprite;

	void Start ()
	{
		sprite = GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		//Vector3 axes = new Vector3 (Input.mousePosition.x, -Input.mousePosition.y, 0);
		//transform.position += axes;
		if (Input.GetAxis ("Fire1")> 0)
		{
			sprite.material.color = Color.red;
		}
		else
		{
			sprite.material.color = Color.blue;
		}
	}
}