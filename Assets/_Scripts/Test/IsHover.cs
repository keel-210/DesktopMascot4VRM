using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsHover : MonoBehaviour
{
	public static bool isHover;
	[SerializeField] Text text;
	void Update ()
	{
		text.text = isHover.ToString ();
	}
}