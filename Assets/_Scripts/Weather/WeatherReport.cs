using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeatherReport : MonoBehaviour
{
	List<WeatherPanel> panels = new List<WeatherPanel>();
	ListRect lRect;
	void Start()
	{
		lRect = gameObject.AddComponent<ListRect>();
		lRect.fixStandard = ListRect.FixedStandard.Top;
		lRect.ListDirection = new Vector3(-1, 0, 0);

		string a = "道北", c = "稚内";
		AddPanel();
		foreach (WeatherPanel p in panels) { p.Init(a, c); }
	}
	void OnEnable()
	{
		foreach (WeatherPanel p in panels)
		{
			p.gameObject.SetActive(true);
		}
	}
	void Update()
	{

	}
	void LoadCityFromPlayerPref()
	{

	}
	public void AddPanel()
	{
		if (panels.Count != 0)
		{
			panels = panels.Where(item => item != null).ToList();
		}
		var obj = (GameObject)Instantiate(Resources.Load("_Prefabs/uGUI/WeatherPanel"));
		obj.transform.parent = transform;
		WeatherPanel p = obj.GetComponent<WeatherPanel>();
		panels.Add(p);
		if (panels.Count == 1)
		{
			p.GetComponent<RectTransform>().anchoredPosition = new Vector3(-150, 50, 0);
		}
		lRect.list.Add(p.GetComponent<RectTransform>());
	}
}