using UnityEngine;
using UnityEngine.UI;

public class LogText : MonoBehaviour
{
	[SerializeField] Text text;
	void Start()
	{
		Application.logMessageReceived += OnLogMessaged;
	}
	void OnLogMessaged(string i_logText, string i_stackTrace, LogType i_type)
	{
		if (string.IsNullOrEmpty(i_logText))
		{
			return;
		}
		text.text += i_logText + System.Environment.NewLine;
	}
}