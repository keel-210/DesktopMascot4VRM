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
	}
	void OnEnable()
	{
		StateChange(OffColor, false);
	}
	void OnDisable()
	{
		StateChange(OffColor, false);
	}
	public void Change()
	{
		if (IsOn)
		{
			StateChange(OffColor, false);
		}
		else
		{
			StateChange(OnColor, true);
		}
	}
	void StateChange(Color c, bool state)
	{
		image.color = c;
		IsOn = state;
	}
}