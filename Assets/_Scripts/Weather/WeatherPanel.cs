using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeatherPanel : MonoBehaviour
{
	[SerializeField] Dropdown Area, City;
	[SerializeField] Text text;
	WeatherData datas;
	void Start ()
	{
		transform.parent = GameObject.Find ("Canvas").transform;
	}
	void OnEnable ()
	{

	}
	public void Init (string area, string city)
	{
		AreaSelect (area);
		CitySelect (city);
		StartCoroutine (Report ());
	}
	public void AreaSelect (string area)
	{
		List<Dropdown.OptionData> options = new List<Dropdown.OptionData> ();
		for (int i = 0; i < WeatherCityID.area.Count; i++)
		{
			Dropdown.OptionData o = new Dropdown.OptionData ();
			o.text = WeatherCityID.area[i];
			options.Add (o);
			if (area == WeatherCityID.area[i])
			{
				Area.value = i;
			}
		}
		Area.options = options;
	}
	void CitySelect (string city)
	{
		List<Dropdown.OptionData> options = new List<Dropdown.OptionData> ();
		for (int i = 0; i < WeatherCityID.citys[Area.value].Count; i++)
		{
			Dropdown.OptionData o = new Dropdown.OptionData ();
			o.text = WeatherCityID.citys[Area.value][i];
			options.Add (o);
			if (city == WeatherCityID.citys[Area.value][i])
			{
				City.value = i;
			}
		}
		City.options = options;
	}
	public void ReportReload ()
	{
		StartCoroutine (Report ());
	}
	IEnumerator Report ()
	{
		int cityName = WeatherCityID.cityID[City.options[City.value].text];
		Weather w = gameObject.AddComponent<Weather> ();
		w.cityNumber = cityName;
		w.Report ();
		Debug.Log (w.weatherData == null);
		if (w.weatherData == null)
		{
			yield return null;
		}
		yield return w.weatherData;
		datas = w.weatherData;
		PlaceText ();
		PlaceImage ();
	}
	void PlaceText ()
	{
		if (datas != null)
		{
			text.text = DateTime.Parse (datas.publicTime).ToString ()+ "\n" + datas.title + "\n" + datas.description.text;
		}
	}
	void PlaceImage ()
	{

	}
	public void RemovePanel ()
	{
		Destroy (gameObject);
	}
}