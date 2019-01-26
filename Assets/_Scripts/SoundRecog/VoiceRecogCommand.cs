using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoiceRecogCommand : MonoBehaviour
{
	public List<SelectMethod> MethodList = new List<SelectMethod>();
	void Start()
	{
		//呼び出し　これは好きなタイミングで呼ぶ
		//IVoiceRecog recog = GetComponent<IVoiceRecog>();
		//recog.OnResult += (text) => Command(text);
		MethodList.ForEach(item => item.Execute());
	}
	void Command(string text)
	{
		// foreach (var c in CommandList)
		// {
		// 	if (text.Contains(c.Command))
		// 	{
		// 		c.Method.Execute();
		// 	}
		// }
	}
	void Compare()
	{

	}
	public void Timer(string text)
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