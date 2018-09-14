using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuoteTest : MonoBehaviour
{
	[SerializeField] Animator animator;
	[SerializeField, Multiline] string QuoteString;
	void Start ()
	{
		FindObjectOfType<QuotePlacer> ().MakeQuote (animator, QuoteString, Color.white, Color.black, HumanBodyBones.Head, new Vector3 (100, 0, 0), new Vector2 (10, 10), AnchorPresets.BottomLeft);
	}
}