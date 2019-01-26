using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VoiceRecogUI : MonoBehaviour
{
	[SerializeField] GameObject Panel;
	[SerializeField] Text text;
	IVoiceRecog recog;
	void Start()
	{
		recog = GetComponent<IVoiceRecog>();
		recog.OnHypothesis += (tex) => text.text = tex;
		recog.OnResult += (tex) => text.text = tex;
	}
	public void ActiveChange()
	{
		if (!Panel.activeInHierarchy)
		{
			Panel.SetActive(true);
			text.text = "";
			recog.Activate();
		}
		else
		{
			Panel.SetActive(false);
			recog.Deactivate();
		}
	}
}