using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Quote : MonoBehaviour
{
	[Multiline] public string QuoteString;
	public Color PanelColor;
	Text text;
	Image image;
	RectTransform tra;
	public void MakeQuote ()
	{
		GameObject imObj = new GameObject ();
		imObj.transform.parent = GameObject.Find ("Canvas").transform;
		image = imObj.AddComponent<Image> ();
		image.color = PanelColor;
		GameObject texObj = new GameObject ();
		tra = texObj.GetComponent<RectTransform> ();
		texObj.transform.parent = GameObject.Find ("Canvas").transform;
		text = texObj.AddComponent<Text> ();
		text.supportRichText = true;
	}
}