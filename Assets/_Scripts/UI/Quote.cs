using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class Quote : MonoBehaviour
{
	public Color PanelColor = Color.white, TextColor = Color.black;
	public HumanBodyBones PlaceBone = HumanBodyBones.Head;
	public Vector2 PlaceOffset = new Vector2 (-100, 0), PanelCollar = new Vector2 (10, 10);
	public AnchorPresets Anchor = AnchorPresets.MiddleCenter;
	public QuoteStyle QuoteStyle;
	public int ListMax;
	public AlphaStyle alphaStyle;
	public float AlphaChangingTime;
	public Vector3 MoveDirection;
	public float MovingTime;
	List<QuoteController> quoteCtrl = new List<QuoteController> ();
	List<Animator> animList = new List<Animator> ();
	public void MakeQuote (Animator animator, string QuoteString, Color PanelColor, Color TextColor,
		HumanBodyBones PlaceBone, Vector2 PlaceOffset, Vector2 PanelCollar, AnchorPresets anchor,
		QuoteStyle quoteStyle, int ListMax, AlphaStyle alphaStyle, float alphaTime,
		Vector3 moveDirection, float movingTime)
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

		if (!animList.Any (a => a == animator))
		{
			animList.Add (animator);
			quoteCtrl.Add (new QuoteController ());
		}
		int CtrlIndex = animList.IndexOf (animator);
		PlaceQuote (animator, PlaceBone, imRect, PlaceOffset, anchor);
		QuoteEnter (new QuoteStyles (imRect, quoteStyle, alphaStyle, ListMax, alphaTime, movingTime, moveDirection), CtrlIndex);
	}
	public void Settings (out Color panelColor, out Color textColor,
		out HumanBodyBones placeBone, out Vector2 placeOffset, out Vector2 panelCollar,
		out AnchorPresets anchor, out QuoteStyle quoteStyle, out int listMax, out AlphaStyle _alphaStyle,
		out float alphaChangingTime, out Vector3 moveDirection, out float movingTime)
	{
		panelColor = PanelColor;
		textColor = TextColor;
		placeBone = PlaceBone;
		placeOffset = PlaceOffset;
		panelCollar = PanelCollar;
		anchor = Anchor;
		quoteStyle = QuoteStyle;
		listMax = ListMax;
		_alphaStyle = alphaStyle;
		alphaChangingTime = AlphaChangingTime;
		moveDirection = MoveDirection;
		movingTime = MovingTime;
	}
	void PlaceQuote (Animator animator, HumanBodyBones PlaceBone, RectTransform panel,
		Vector3 PlaceOffset, AnchorPresets anchor)
	{

		if (PlaceBone != HumanBodyBones.LastBone)
		{
			panel.SetAnchor (anchor);
			panel.position = Camera.main.WorldToScreenPoint (animator.GetBoneTransform (PlaceBone).position)+
				PlaceOffset + new Vector3 (panel.sizeDelta.x, panel.sizeDelta.y, 0);
		}
	}
	void QuoteEnter (QuoteStyles styles, int ctrlIndex)
	{
		switch (styles.quoteStyle)
		{
			case QuoteStyle.OnlyOne:
				if (quoteCtrl[ctrlIndex].list.Count > 0)
				{
					foreach (QuoteStyles s in quoteCtrl[ctrlIndex].list)
					{
						QuoteExit (s, ctrlIndex);
					}
				}
				break;
			case QuoteStyle.Time:
				StartCoroutine (this.DelayMethod (3f, ()=> { QuoteExit (styles, ctrlIndex); }));
				break;
			case QuoteStyle.List:
				if (quoteCtrl[ctrlIndex].list.Count > styles.ListMax)
				{
					QuoteExit (quoteCtrl[ctrlIndex].list[0], ctrlIndex);
				}
				break;
			default:
				break;
		}
		switch (styles.alphaStyle)
		{
			case AlphaStyle.AlphaAlpha:
			case AlphaStyle.AlpahDefault:
				AlphaController alpha = styles.panel.gameObject.AddComponent<AlphaController> ();
				alpha.Init (Color.black, styles.alphaTime, 0, false);
				break;
		}
		if (styles.moveDirection != Vector3.zero)
		{
			RectPositionMover rect = styles.panel.gameObject.AddComponent<RectPositionMover> ();
			rect.Init (-styles.moveDirection, Vector3.zero, 0, styles.moveTime);
		}
		quoteCtrl[ctrlIndex].list.Add (styles);
	}
	void QuoteExit (QuoteStyles styles, int ctrlIndex)
	{
		quoteCtrl[ctrlIndex].list.Remove (styles);
		switch (styles.alphaStyle)
		{
			case AlphaStyle.AlphaAlpha:
			case AlphaStyle.DefaultAlpha:
				AlphaController alpha = styles.panel.gameObject.AddComponent<AlphaController> ();
				alpha.Init (Color.black, styles.alphaTime, 0, true);
				break;
			default:
				Destroy (styles.panel, styles.alphaTime > styles.moveTime ? styles.alphaTime : styles.moveTime);
				break;
		}
		if (styles.moveDirection != Vector3.zero)
		{
			RectPositionMover rect = styles.panel.gameObject.AddComponent<RectPositionMover> ();
			rect.Init (-styles.moveDirection, Vector3.zero, 0, styles.moveTime);
		}
	}
}