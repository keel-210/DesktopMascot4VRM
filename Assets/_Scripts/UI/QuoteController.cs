using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class QuoteController : MonoBehaviour
{
	public List<QuoteStyles> list = new List<QuoteStyles> ();
	List<QuoteStyles> ListQuotes = new List<QuoteStyles> ();
	int prevCount = 0;
	RectTransform LastListRect;
	List<Vector3> ListPos = new List<Vector3> ();
	void Update ()
	{
		if (prevCount != list.Count)
		{
			prevCount = list.Count;
			ListQuotes = list.Where (s => s.quoteStyle == QuoteStyle.List).ToList ();
			LastListRect = ListQuotes.Last ().panel;
			ListQuotes.RemoveAt (ListQuotes.Count - 1);
			ListPos = list.Select (s => s.panel.position).ToList ();
		}
		if (prevCount > 0)
		{
			for (int i = 0; i < ListQuotes.Count; i++)
			{
				ListQuotes[i].panel.position = Vector3.Lerp (ListQuotes[i].panel.position,
					ListPos[i] + new Vector3 (LastListRect.sizeDelta.x, LastListRect.sizeDelta.y, 0), 0.1f);
			}
		}
	}
}
public class QuoteStyles
{
	public RectTransform panel;
	public QuoteStyle quoteStyle;
	public AlphaStyle alphaStyle;
	public float ListMax, alphaTime, moveTime;
	public Vector3 moveDirection;
	public QuoteStyles (RectTransform _panel, QuoteStyle _quoteStyle, AlphaStyle _alphaStyle,
		float _listMax, float _alphaTime, float _moveTime, Vector3 _moveDirection)
	{
		panel = _panel;
		quoteStyle = _quoteStyle;
		alphaStyle = _alphaStyle;
		ListMax = _listMax;
		alphaTime = _alphaTime;
		moveTime = _moveTime;
		moveDirection = _moveDirection;
	}
}