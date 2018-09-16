using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaController : MonoBehaviour
{
	List<CanvasRenderer> rends = new List<CanvasRenderer> ();
	public Color ColorOffset;
	public float DelayTime = 3;
	float Timer;
	public AlphaController (Color c, float Delay)
	{
		ColorOffset = c;
		DelayTime = Delay;
	}
	void Start ()
	{
		rends = new List<CanvasRenderer> ();
		List<GameObject> list = GetAllChildren.GetAll (gameObject);
		list.Add (gameObject);
		foreach (GameObject obj in list)
		{
			CanvasRenderer r = obj.GetComponent<CanvasRenderer> ();
			Debug.Log (r);
			if (r != null)
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