using System.Collections;
using UnityEngine;

public class Weather : MonoBehaviour
{
	public WeatherData weatherData { get; private set; }
	const string baseURL = "http://weather.livedoor.com/forecast/webservice/json/v1";
	//東京都のID
	public int cityNumber = 130010;

	public Weather (int _cityNumber)
	{
		cityNumber = _cityNumber;
		Report ();
	}
	public void Report ()
	{
		string url = baseURL + "?city=" + cityNumber.ToString ("D6");
		StartCoroutine (Get (url));
	}
	IEnumerator Get (string url)
	{
		WWW www = new WWW (url);
		if (!string.IsNullOrEmpty (www.error))
		{

			Debug.LogError ("www Error:" + www.error);
			yield break;

		}
		yield return www;
		WeatherData data = JsonUtility.FromJson<WeatherData> (www.text);
		weatherData = data;
	}
}