using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour
{
	[SerializeField] Color OnColor = Color.white, OffColor = Color.black;
	Image image;
	bool IsOn;
	void Start()
	{
		image = GetComponent<Image>();
		StateChange(OffColor, false);
	}
	void OnEnable()
	{
		if (IsOn)
			StateChange(OffColor, false);
	}
	void OnDisable()
	{
		if (IsOn)
			StateChange(OffColor, false);
	}
	public void Change()
	{
		if (IsOn)
		{
			StateChange(OffColor, !IsOn);
		}
		else
		{
			StateChange(OnColor, !IsOn);
		}
	}
	void StateChange(Color c, bool state)
	{
		image.color = c;
		IsOn = state;
	}
}