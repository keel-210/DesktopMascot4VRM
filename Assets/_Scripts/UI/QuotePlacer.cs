using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuotePlacer : MonoBehaviour
{
	List<RectTransform> QuoteList = new List<RectTransform> ();
	void Update ()
	{

	}
	public void PlaceQuote (RectTransform panel)
	{
		QuoteList.Add (panel);
	}
	public void MakeQuote (string QuoteString, Color PanelColor)
	{
		GameObject imObj = new GameObject ();
		imObj.AddComponent<RectTransform> ();
		imObj.transform.parent = GameObject.Find ("Canvas").transform;
		Image image = imObj.AddComponent<Image> ();
		image.color = PanelColor;
		GameObject texObj = new GameObject ();
		RectTransform tra = texObj.AddComponent<RectTransform> ();
		texObj.transform.parent = imObj.transform;
		Text text = texObj.AddComponent<Text> ();
		text.supportRichText = true;
		text.text = QuoteString;
		PlaceQuote (imObj.GetComponent<RectTransform> ());
	}
}