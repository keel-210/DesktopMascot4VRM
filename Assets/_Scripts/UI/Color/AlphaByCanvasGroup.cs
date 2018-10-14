using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlphaByCanvasGroup : MonoBehaviour
{
	CanvasGroup group;
	float DelayTime, ChangingTime;
	bool MinusColor;
	float Timer, ColorDirection;
	void Start()
	{
		group = GetComponent<CanvasGroup>();
	}
	public void Init(float changing, float Delay, bool minus)
	{
		group = GetComponent<CanvasGroup>();
		ChangingTime = changing;
		DelayTime = Delay;
		MinusColor = minus;
		if (MinusColor)
		{
			ColorDirection = -1 / changing;
		}
		else
		{
			ColorDirection = 1 / changing;
			group.alpha = 0;
		}
		Timer = 0;
	}
	void Update()
	{
		if (Timer <= DelayTime)
		{
			Timer += Time.deltaTime;
		}
		else if (Timer <= DelayTime + ChangingTime)
		{
			Timer += Time.deltaTime;
			group.alpha += ColorDirection * Time.deltaTime;
		}
		else
		{
			if (ColorDirection > 0)
			{
				group.alpha = 1;
			}
			else
			{
				group.alpha = 0;
			}
			Destroy(this);
		}
	}
	void OnDisable()
	{
		Destroy(this);
	}
}