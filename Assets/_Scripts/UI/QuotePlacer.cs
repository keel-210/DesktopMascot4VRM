using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuotePlacer : MonoBehaviour
{
	RectTransform quote;
	void PlaceQuote (Animator animator, HumanBodyBones PlaceBone, RectTransform panel, Vector3 PlaceOffset, AnchorPresets anchor)
	{
		if (quote)
		{
			Destroy (quote.gameObject);
		}
		quote = panel;
		if (PlaceBone != HumanBodyBones.LastBone)
		{
			panel.SetAnchor (anchor);
			panel.position = Camera.main.WorldToScreenPoint (animator.GetBoneTransform (PlaceBone).position)+ PlaceOffset;
		}
	}
	public void MakeQuote (Animator animator, string QuoteString)
	{
		MakeQuote (animator, QuoteString, Color.white, Color.black, HumanBodyBones.Head, new Vector2 (-100, 0), new Vector2 (10, 10), AnchorPresets.BottomLeft);
	}
	public void MakeQuote (Animator animator, string QuoteString, Color PanelColor)
	{
		MakeQuote (animator, QuoteString, PanelColor, Color.black, HumanBodyBones.Head, new Vector2 (-100, 0), new Vector2 (10, 10), AnchorPresets.BottomLeft);
	}
	public void MakeQuote (Animator animator, string QuoteString, Color PanelColor, Color TextColor, HumanBodyBones PlaceBone, Vector2 PlaceOffset, Vector2 PanelCollar, AnchorPresets anchor)
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
		text.font = Font.CreateDynamicFontFromOSFont ("Arial", 20);
		text.text = QuoteString;
		text.color = TextColor;

		text.rectTransform.sizeDelta = new Vector2 (text.preferredWidth, text.preferredHeight);
		text.rectTransform.sizeDelta = new Vector2 (text.preferredWidth, text.preferredHeight);
		image.rectTransform.sizeDelta = text.rectTransform.sizeDelta + PanelCollar;

		PlaceQuote (animator, PlaceBone, imObj.GetComponent<RectTransform> (), PlaceOffset, anchor);
	}
}