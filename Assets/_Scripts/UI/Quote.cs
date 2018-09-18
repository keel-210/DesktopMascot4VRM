using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Quote : MonoBehaviour
{
	List<RectTransform> list = new List<RectTransform> ();
	RectTransform quote;

	public void MakeQuote (Animator animator, string QuoteString, Color PanelColor, Color TextColor, HumanBodyBones PlaceBone, Vector2 PlaceOffset, Vector2 PanelCollar, AnchorPresets anchor)
	{
		GameObject imObj = new GameObject ();
		RectTransform imRect = imObj.AddComponent<RectTransform> ();
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

		PlaceQuote (animator, PlaceBone, imRect, PlaceOffset, anchor);
	}
	void PlaceQuote (Animator animator, HumanBodyBones PlaceBone, RectTransform panel, Vector3 PlaceOffset, AnchorPresets anchor)
	{
		if (quote)
		{
			//Destroy (quote.gameObject);
		}
		quote = panel;
		if (PlaceBone != HumanBodyBones.LastBone)
		{
			panel.SetAnchor (anchor);
			panel.position = Camera.main.WorldToScreenPoint (animator.GetBoneTransform (PlaceBone).position)+
				PlaceOffset + new Vector3 (panel.sizeDelta.x, panel.sizeDelta.y, 0);
		}
	}
	void QuoteEnter ()
	{

	}
	void QuoteExit ()
	{

	}
}