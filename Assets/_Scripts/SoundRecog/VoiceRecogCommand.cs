using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoiceRecogCommand : MonoBehaviour
{
	[System.Serializable]
	public class Command
	{
		public string Key { get; set; }
		public SelectMethod Method { get; set; }
	}
	public List<Command> CommandList;
	void Start()
	{
		// IVoiceRecog recog = GetComponent<IVoiceRecog>();
		// recog.OnResult += (text) => CommandCheck(text);
	}
	void CommandCheck(string text)
	{
		foreach (var c in CommandList)
		{
			if (text.Contains(c.Key))
			{
				c.Method.Execute();
			}
		}
	}
	void Compare()
	{

	}
	public void Timer()
	{

	}
	public void Alarm()
	{

	}
	public void Weather()
	{

	}
	public void Lancher()
	{

	}
}