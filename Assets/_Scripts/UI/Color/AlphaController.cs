using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlphaController : MonoBehaviour
{
	List<CanvasRenderer> rends = new List<CanvasRenderer> ();
	Color ColorOffset;
	float DelayTime = 3, ChangingTime;
	bool MinusColor;
	float Timer, ColorDirection;
	public void Init (Color c, float changing, float Delay, bool minus)
	{
		List<GameObject> list = GetAllChildren.GetAll (gameObject);
		list.Add (gameObject);
		rends = list.Select (x => x.GetComponent<CanvasRenderer> ()).Where (r => r != null).ToList ();
		ColorOffset = c / changing;
		DelayTime = Delay;
		MinusColor = minus;
		if (MinusColor)
		{
			ColorDirection = -1;
		}
		else
		{
			ColorDirection = 1;
			foreach (CanvasRenderer r in rends)
			{
				Color color = r.GetColor ();
				color = new Color (color.r, color.g, color.b, 0);
				r.SetColor (color);
			}
		}
	}
	void Start ()
	{
		List<GameObject> list = GetAllChildren.GetAll (gameObject);
		list.Add (gameObject);
		rends = list.Select (x => x.GetComponent<CanvasRenderer> ()).Where (r => r != null).ToList ();
	}
	void Update ()
	{
		if (Timer < DelayTime)
		{
			Timer += Time.deltaTime;
		}
		else
		{
			foreach (CanvasRenderer r in rends)
			{
				Color c = r.GetColor ()+ ColorDirection * ColorOffset * Time.deltaTime;
				r.SetColor (c);
			}
			if (rends[0].GetColor ().a <= 0 && MinusColor)
			{
				Destroy (gameObject);
			}
			if (rends[0].GetColor ().a >= 1 && !MinusColor)
			{
				Destroy (this);
			}
		}
	}
}