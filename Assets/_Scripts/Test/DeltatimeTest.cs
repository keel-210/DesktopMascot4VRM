using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeltatimeTest : MonoBehaviour
{
	[SerializeField] Text text;

	void Update ()
	{
		text.text = Time.timeScale.ToString ();
	}
}