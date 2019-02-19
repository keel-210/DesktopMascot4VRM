using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VoiceRecogUI : MonoBehaviour
{
	[SerializeField] GameObject Panel;
	[SerializeField] Text text;
	string TextCash;
	IVoiceRecog recog;
	void Start()
	{
		recog = GetComponent<IVoiceRecog>();
		Panel.SetActive(false);
	}
	void Update()
	{
		text.text = TextCash;
	}
	void OnDisable()
	{
		if (recog != null)
			recog.Deactivate();
	}
	public void ActiveChange()
	{
		Debug.Log("Active Change");
		if (!Panel.activeInHierarchy)
		{
			Panel.SetActive(true);
			text.text = "";
			recog.OnHypothesis += (tex) => TextCash = tex;
			recog.OnResult += (tex) =>
			{
				TextCash = tex;
				Debug.Log(TextCash);
			};
			recog.Activate();
		}
		else
		{
			Panel.SetActive(false);
			recog.Deactivate();
		}
	}
}