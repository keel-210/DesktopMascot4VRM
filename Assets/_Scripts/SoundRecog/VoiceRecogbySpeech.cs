using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
public class VoiceRecogbySpeech : MonoBehaviour, IVoiceRecog
{
	public string RecogResult { get; private set; }
	private DictationRecognizer dictationRecognizer;
	public Action<string> OnResult { get; set; }
	public Action<string> OnHypothesis { get; set; }
	public void Activate()
	{
		try
		{
			ActiveteRecog();
		}
		catch
		{
			DeActivateRecog();
			FindObjectOfType<ErrorReciever>().Error("Error 601 : Windows.Speech Activate Error");
		}
	}
	public void Deactivate()
	{
		DeActivateRecog();
	}
	private void ActiveteRecog()
	{
		dictationRecognizer = new DictationRecognizer();
		dictationRecognizer.DictationResult += (text, confidence) => OnResult(text);
		dictationRecognizer.DictationHypothesis += (text) => OnHypothesis(text);

		dictationRecognizer.Start();
	}

	private void DeActivateRecog()
	{
		if (dictationRecognizer != null)
		{
			dictationRecognizer.DictationResult -= (text, confidence) => RecogResult = text;
			dictationRecognizer.DictationHypothesis -= (text) => RecogResult = text;
			dictationRecognizer.Dispose();
		}
	}
}