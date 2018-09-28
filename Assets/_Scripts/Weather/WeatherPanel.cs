using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeatherPanel : MonoBehaviour
{
	Dropdown Area, City;
	Text text;
	WeatherData datas;
	void Start ()
	{
		transform.parent = GameObject.Find ("Canvas").transform;
		gameObject.AddComponent<RectTransform> ();
		gameObject.AddComponent<Image> ();
		var areaDropDown = (GameObject)Instantiate (Resources.Load ("_Prefabs/uGUI/DropDown"));
		Area = areaDropDown.GetComponent<Dropdown> ();
		var cityDropDown = (GameObject)Instantiate (Resources.Load ("_Prefabs/uGUI/DropDown"));
		City = cityDropDown.GetComponent<Dropdown> ();
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
			options[i].text = WeatherCityID.area[i];
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
			options[i].text = WeatherCityID.citys[Area.value][i];
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
		var cityName = City.options[City.value].text;
		var cityNum = WeatherCityID.cityID[cityName];
		Weather w = new Weather (cityNum);
		yield return w.weatherData;
		datas = w.weatherData;
		PlaceText ();
		PlaceImage ();
	}
	void PlaceText ()
	{
		text.text = DateTime.Parse (datas.publicTime).ToString ()+ "\n" + datas.title + "\n" + datas.description.text;
	}
	void PlaceImage ()
	{

	}
	public void RemovePanel ()
	{
		Destroy (gameObject);
	}
}