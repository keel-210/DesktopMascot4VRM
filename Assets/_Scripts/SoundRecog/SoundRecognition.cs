using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SoundRecognition : MonoBehaviour
{
	private DictationRecognizer dictationRecognizer;

	private void Start()
	{
		dictationRecognizer = new DictationRecognizer();
		dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
		dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;
		dictationRecognizer.DictationComplete += DictationRecognizer_DictationComplete;
		dictationRecognizer.DictationError += DictationRecognizer_DictationError;

		dictationRecognizer.Start();
	}

	private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
	{
		Debug.Log("DictationResult: " + text);
	}

	private void DictationRecognizer_DictationHypothesis(string text)
	{
		//Debug.Log("DictationHypothesis: " + text);
	}

	private void DictationRecognizer_DictationComplete(DictationCompletionCause cause)
	{
		Debug.Log("DictationComplete: " + cause);
	}

	private void DictationRecognizer_DictationError(string error, int hresult)
	{
		Debug.Log("DictationError: " + error);
	}

	private void OnDisable()
	{
		dictationRecognizer.DictationResult -= DictationRecognizer_DictationResult;
		dictationRecognizer.DictationComplete -= DictationRecognizer_DictationComplete;
		dictationRecognizer.DictationHypothesis -= DictationRecognizer_DictationHypothesis;
		dictationRecognizer.DictationError -= DictationRecognizer_DictationError;
		dictationRecognizer.Dispose();
	}
}