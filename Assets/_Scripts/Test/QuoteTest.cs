using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuoteTest : MonoBehaviour
{

	void Start ()
	{
		FindObjectOfType<QuotePlacer> ().MakeQuote ("aaaaaaaaaa", Color.white);
	}
}