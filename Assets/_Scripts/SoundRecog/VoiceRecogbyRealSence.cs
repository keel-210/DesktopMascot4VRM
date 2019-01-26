using UnityEngine;

public class VoiceRecogbyRealSence : MonoBehaviour, IVoiceRecog
{
	private PXCMSession session;
	private PXCMSpeechRecognition sr;
	public System.Action<string> OnResult { get; set; }
	public System.Action<string> OnHypothesis { get; set; }
	public void Activate()
	{
		try
		{
			ActivateRecog();
		}
		catch
		{
			FindObjectOfType<ErrorReciever>().Error("Error 600 : IntelRealsence Activate Error");
		}
	}
	public void Deactivate()
	{
		if (sr != null)
		{
			sr.StopRec();
			sr.Dispose();
		}
		if (session != null)
		{
			session.Dispose();
		}
	}
	void ActivateRecog()
	{
		session = PXCMSession.CreateInstance();
		PXCMAudioSource source = session.CreateAudioSource();

		PXCMAudioSource.DeviceInfo dinfo = null;

		source.QueryDeviceInfo(1, out dinfo);
		source.SetDevice(dinfo);
		Debug.Log(dinfo.name);

		session.CreateImpl<PXCMSpeechRecognition>(out sr);

		PXCMSpeechRecognition.ProfileInfo pinfo;
		sr.QueryProfile(out pinfo);
		pinfo.language = PXCMSpeechRecognition.LanguageType.LANGUAGE_JP_JAPANESE;
		sr.SetProfile(pinfo);

		PXCMSpeechRecognition.Handler handler = new PXCMSpeechRecognition.Handler();
		handler.onRecognition = (x) => OnResult(x.scores[0].sentence);
		sr.SetDictation();
		sr.StartRec(source, handler);
	}
}