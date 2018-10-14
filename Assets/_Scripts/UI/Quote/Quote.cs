using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class Quote : MonoBehaviour
{
	public Color PanelColor = Color.white, TextColor = Color.black;
	public HumanBodyBones PlaceBone = HumanBodyBones.Head;
	public Vector2 PlaceOffset = new Vector2(-100, 0), PanelCollar = new Vector2(10, 10);
	public AnchorPresets Anchor = AnchorPresets.MiddleCenter;
	public PivotPresets Pivot = PivotPresets.MiddleCenter;
	public QuoteStyle QuoteStyle;
	public int ListMax;
	public AlphaStyle alphaStyle;
	public float AlphaChangingTime;
	public Vector3 MoveDirection;
	public float MovingTime;
	public BackImage BackImg;
	public int FontSize;
	public Font font;
	List<QuoteController> quoteCtrl = new List<QuoteController>();
	public void MakeQuote(Animator animator, string QuoteString, Color PanelColor, Color TextColor,
		HumanBodyBones PlaceBone, Vector2 PlaceOffset, Vector2 PanelCollar, AnchorPresets anchor,
		PivotPresets pivot, QuoteStyle quoteStyle, int ListMax, AlphaStyle alphaStyle, BackImage backImage, float alphaTime,
		Vector3 moveDirection, float movingTime, int fontSize, Font font)
	{
		GameObject imObj = new GameObject();
		RectTransform imRect = imObj.AddComponent<RectTransform>();
		imRect.SetPivot(pivot);
		imRect.SetAnchor(anchor);
		imObj.transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
		Image image = imObj.AddComponent<Image>();
		image.color = PanelColor;
		if (backImage == BackImage.None)
		{
			image.color = new Color(0, 0, 0, 0);
		}
		else
		{
			image.type = Image.Type.Sliced;
			image.sprite = Resources.Load<Sprite>("_Sprites/" + backImage.ToString());
		}

		GameObject texObj = new GameObject();
		RectTransform tra = texObj.AddComponent<RectTransform>();
		texObj.transform.SetParent(imObj.GetComponent<RectTransform>());
		Text text = texObj.AddComponent<Text>();
		text.supportRichText = true;
		text.font = font;
		text.fontSize = fontSize;
		text.text = QuoteString;
		text.color = TextColor;

		text.rectTransform.sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
		text.rectTransform.sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
		image.rectTransform.sizeDelta = text.rectTransform.sizeDelta + PanelCollar;
		tra.anchoredPosition = Vector2.zero;

		if (!quoteCtrl.Select(q => q.anim).Any(a => a == animator)|| quoteCtrl.Count == 0)
		{
			QuoteController c = gameObject.AddComponent<QuoteController>();
			c.anim = animator;
			quoteCtrl.Add(c);
		}
		int CtrlIndex = quoteCtrl.Select(q => q.anim).ToList().IndexOf(animator);
		QuoteEnter(new QuoteStyles(imRect, PlaceBone, PlaceOffset, quoteStyle, alphaStyle, ListMax, alphaTime, movingTime, moveDirection), CtrlIndex);
		imObj.transform.localScale = Vector3.one;
		//imObj.SetActive(false);
		//imObj.SetActive(true);
	}
	public void Settings(out Color panelColor, out Color textColor,
		out HumanBodyBones placeBone, out Vector2 placeOffset, out Vector2 panelCollar,
		out AnchorPresets anchor, out PivotPresets pivot, out QuoteStyle quoteStyle, out int listMax, out AlphaStyle _alphaStyle,
		out float alphaChangingTime, out Vector3 moveDirection, out float movingTime,
		out BackImage backImage, out int fontSize, out Font fonts)
	{
		panelColor = PanelColor;
		textColor = TextColor;
		placeBone = PlaceBone;
		placeOffset = PlaceOffset;
		panelCollar = PanelCollar;
		anchor = Anchor;
		pivot = Pivot;
		quoteStyle = QuoteStyle;
		listMax = ListMax;
		_alphaStyle = alphaStyle;
		alphaChangingTime = AlphaChangingTime;
		moveDirection = MoveDirection;
		movingTime = MovingTime;
		backImage = BackImg;
		fontSize = FontSize;
		fonts = font;
	}
	void QuoteEnter(QuoteStyles styles, int ctrlIndex)
	{
		switch (styles.quoteStyle)
		{
			case QuoteStyle.OnlyOne:
				quoteCtrl[ctrlIndex].PlaceQuoteOnBone(styles);
				if (quoteCtrl[ctrlIndex].list.Count > 0)
				{
					while (quoteCtrl[ctrlIndex].list.Count != 0)
					{
						QuoteExit(quoteCtrl[ctrlIndex].list[0], ctrlIndex);
					}
				}
				break;
			case QuoteStyle.Time:
				quoteCtrl[ctrlIndex].PlaceQuoteOnBone(styles);
				StartCoroutine(this.DelayMethod(3f, ()=> { QuoteExit(styles, ctrlIndex); }));
				break;
			case QuoteStyle.List:
				if (quoteCtrl[ctrlIndex].list.Count > styles.ListMax - 1)
				{
					QuoteExit(quoteCtrl[ctrlIndex].list[0], ctrlIndex);
				}
				quoteCtrl[ctrlIndex].UpdateList(styles);
				break;
			default:
				break;
		}
		switch (styles.alphaStyle)
		{
			case AlphaStyle.AlphaAlpha:
			case AlphaStyle.AlpahDefault:
				AlphaController alpha = styles.panel.gameObject.AddComponent<AlphaController>();
				alpha.Init(styles.alphaTime, 0, false);
				break;
		}
		if (styles.moveDirection != Vector3.zero)
		{
			RectPositionMover rect = styles.panel.gameObject.AddComponent<RectPositionMover>();
			rect.Init(-styles.moveDirection, Vector3.zero, 0, styles.moveTime);
		}
		quoteCtrl[ctrlIndex].list.Add(styles);
	}
	void QuoteExit(QuoteStyles styles, int ctrlIndex)
	{
		quoteCtrl[ctrlIndex].list.Remove(styles);
		switch (styles.alphaStyle)
		{
			case AlphaStyle.AlphaAlpha:
			case AlphaStyle.DefaultAlpha:
				AlphaController alpha = styles.panel.gameObject.AddComponent<AlphaController>();
				alpha.Init(styles.alphaTime, 0, true);
				styles.panel.gameObject.AddComponent<AlphaDestroy>();
				break;
			default:
				Debug.Log("Delete");
				Destroy(styles.panel.gameObject, styles.alphaTime >= styles.moveTime ? styles.alphaTime : styles.moveTime);
				break;
		}
		if (styles.moveDirection != Vector3.zero)
		{
			RectPositionMover rect = styles.panel.gameObject.AddComponent<RectPositionMover>();
			rect.Init(Vector3.zero, styles.moveDirection, 0, styles.moveTime);
		}
	}
}