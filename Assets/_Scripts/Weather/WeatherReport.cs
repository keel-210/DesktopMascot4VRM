using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeatherReport : MonoBehaviour
{
	List<WeatherPanel> panels = new List<WeatherPanel> ();
	void Start ()
	{
		string a = "道北", c = "稚内";
		foreach (WeatherPanel p in panels) { p.Init (a, c); }
	}
	void Update ()
	{

	}
	void LoadCityFromPP ()
	{

	}
	public void AddPanel ()
	{
		panels = panels.Where (item => item != null).ToList ();
		var obj = new GameObject ();
		WeatherPanel p = obj.AddComponent<WeatherPanel> ();
		panels.Add (p);
	}
}