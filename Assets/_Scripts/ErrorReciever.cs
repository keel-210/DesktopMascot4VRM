using UnityEngine;
using UnityEngine.UI;
public class ErrorReciever : MonoBehaviour
{
	[SerializeField] GameObject ErrorPanel;
	[SerializeField] Text ErrorText;
	public void Error(string errorLog)
	{
		if (ErrorPanel && ErrorText)
		{
			ErrorPanel.SetActive(true);
			ErrorText.text = errorLog;
		}
		else
		{
			MakeCanvas();
		}
	}
	void MakeCanvas()
	{

	}
}