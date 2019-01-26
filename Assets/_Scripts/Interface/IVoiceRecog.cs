using System;
public interface IVoiceRecog
{
	Action<string> OnHypothesis { get; set; }
	Action<string> OnResult { get; set; }
	void Activate();
	void Deactivate();
}