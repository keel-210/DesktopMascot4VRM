using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlphaController : MonoBehaviour
{
	List<CanvasRenderer> rends = new List<CanvasRenderer>();
	List<float> alphas = new List<float>();
	Color ColorOffset;
	float DelayTime, ChangingTime;
	bool MinusColor;
	float Timer, ColorDirection;
	public void Init(float changing, float Delay, bool minus)
	{
		List<GameObject> list = GetAllChildren.GetAll(gameObject);
		rends = list.Select(x => x.GetComponent<CanvasRenderer>()).Where(r => r != null).ToList();
		alphas = rends.Select(item => item.GetAlpha()/ changing).ToList();
		ChangingTime = changing;
		DelayTime = Delay;
		MinusColor = minus;
		if (MinusColor)
		{
			ColorDirection = -1;
		}
		else
		{
			ColorDirection = 1;
			rends.ForEach(r => r.SetAlpha(0));
		}
		Timer = 0;
	}
	void Start()
	{
		List<GameObject> list = GetAllChildren.GetAll(gameObject);
		list.Add(gameObject);
	}
	void Update()
	{
		if (Timer <= DelayTime)
		{
			Timer += Time.deltaTime;
			if (!MinusColor)
			{
				rends.ForEach(r => r.SetAlpha(0));
			}
		}
		else if (Timer <= DelayTime + ChangingTime)
		{
			Timer += Time.deltaTime;
			for (int i = 0; i < rends.Count; i++)
			{
				float c = rends[i].GetAlpha()+ ColorDirection * alphas[i] * Time.deltaTime;
				rends[i].SetAlpha(c);
			}
		}
		else
		{
			for (int i = 0; i < rends.Count; i++)
			{
				rends[i].SetAlpha(alphas[i] * ChangingTime);
			}
			Destroy(this);
		}
	}
	void OnDisable()
	{
		Destroy(this);
	}
}