using System;

[Serializable]
public class WeatherData
{
	public Pinpointlocation[] pinpointLocations;
	public string link;
	public Forecast[] forecasts;
	public Location location;
	public string publicTime;
	public Copyright copyright;
	public string title;
	public Description description;
}

[Serializable]
public class Location
{
	public string city;
	public string area;
	public string prefecture;
}

[Serializable]
public class Copyright
{
	public Provider[] provider;
	public string link;
	public string title;
	public Image4W image;
}

[Serializable]
public class Image4W
{
	public int width;
	public string link;
	public string url;
	public string title;
	public int height;
}

[Serializable]
public class Provider
{
	public string link;
	public string name;
}

[Serializable]
public class Description
{
	public string text;
	public string publicTime;
}

[Serializable]
public class Pinpointlocation
{
	public string link;
	public string name;
}

[Serializable]
public class Forecast
{
	public string dateLabel;
	public string telop;
	public string date;
	public Temperature temperature;
	public Image1 image;
}

[Serializable]
public class Temperature
{
	public Min min;
	public Max max;
}

[Serializable]
public class Min
{
	public string celsius;
	public string fahrenheit;
}

[Serializable]
public class Max
{
	public string celsius;
	public string fahrenheit;
}

[Serializable]
public class Image1
{
	public int width;
	public string url;
	public string title;
	public int height;
}