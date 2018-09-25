using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class QuoteController : MonoBehaviour
{
	public Animator anim;
	public List<QuoteStyles> list = new List<QuoteStyles> ();
	List<QuoteStyles> ListQuotes = new List<QuoteStyles> ();
	int prevCount = 0;
	public RectTransform LastListRect;
	public List<Vector3> ListPos = new List<Vector3> ();
	public void UpdateList (QuoteStyles styles)
	{
		prevCount = list.Count;
		ListQuotes = list.Where (s => s.quoteStyle == QuoteStyle.List).ToList ();
		LastListRect = styles.panel;
		ListQuotes.Remove (styles);
		styles.panel.position = Camera.main.WorldToScreenPoint (anim.GetBoneTransform (styles.bone).position)+ styles.PosOffset;
		ListPos = ListQuotes.Where (item => item.panel != null).Select (s => s.panel.position).ToList ();
	}
	void Update ()
	{
		if (prevCount > 0)
		{
			for (int i = 0; i < ListQuotes.Count; i++)
			{
				ListQuotes = ListQuotes.Where (item => item.panel != null).ToList ();
				ListQuotes[i].panel.position = Vector3.Lerp (ListQuotes[i].panel.position,
					ListPos[i] + new Vector3 (0, LastListRect.sizeDelta.y, 0), 0.1f);
			}
		}
		foreach (QuoteStyles s in list)
		{
			if (s.bone != HumanBodyBones.LastBone)
			{
				s.panel.position = new Vector3 (Camera.main.WorldToScreenPoint (anim.GetBoneTransform (s.bone).position).x + s.PosOffset.x, s.panel.position.y, 0);
			}
		}

	}
}
public class QuoteStyles
{
	public RectTransform panel;
	public HumanBodyBones bone;
	public Vector3 PosOffset;
	public QuoteStyle quoteStyle;
	public AlphaStyle alphaStyle;
	public float ListMax, alphaTime, moveTime;
	public Vector3 moveDirection;
	public QuoteStyles (RectTransform _panel, HumanBodyBones _bone, Vector3 _PosOffset, QuoteStyle _quoteStyle, AlphaStyle _alphaStyle,
		float _listMax, float _alphaTime, float _moveTime, Vector3 _moveDirection)
	{
		panel = _panel;
		bone = _bone;
		PosOffset = _PosOffset;
		quoteStyle = _quoteStyle;
		alphaStyle = _alphaStyle;
		ListMax = _listMax;
		alphaTime = _alphaTime;
		moveTime = _moveTime;
		moveDirection = _moveDirection;
	}
}