using UnityEngine;

public class WeatherTest : MonoBehaviour
{
	Weather w;
	void Start ()
	{
		w = gameObject.AddComponent<Weather> ();
		w.cityNumber = 011000;
		w.Report ();
	}
	void Update ()
	{
		if (w.weatherData != null)
		{
			Debug.Log (w.weatherData.publicTime);
		}
	}
}