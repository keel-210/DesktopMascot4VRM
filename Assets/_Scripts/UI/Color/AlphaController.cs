using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaController : MonoBehaviour
{
	List<CanvasRenderer> rends = new List<CanvasRenderer> ();
	Color ColorOffset;
	float DelayTime;
	float Timer;
	public AlphaController (Color c, float Delay)
	{
		ColorOffset = c;
		DelayTime = Delay;
	}
	void Start ()
	{
		List<GameObject> list = GetAllChildren.GetAll (gameObject);
		foreach (GameObject obj in list)
		{
			CanvasRenderer r = obj.GetComponent<CanvasRenderer> ();
			if (r)
			{
				rends.Add (r);
			}
		}
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
				Color c = r.GetColor ()- ColorOffset * Time.deltaTime;
				r.SetColor (c);
			}
			if (rends[0].GetColor ().a <= 0)
			{
				Destroy (gameObject);
			}
		}
	}
}