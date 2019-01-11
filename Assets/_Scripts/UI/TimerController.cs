using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
	public float time;
	[SerializeField] Text text;
	void Start()
	{
		transform.parent = GameObject.Find("Canvas").transform;
		GetComponent<RectTransform>().localPosition = Vector2.zero;
		GetComponent<RectTransform>().SetAnchor(AnchorPresets.BottomCenter, 0, 0);
	}

	void Update()
	{
		text.text = (((int)time / 60)).ToString("D2") + ":" + (((int)time % 60)).ToString("D2");
	}
}