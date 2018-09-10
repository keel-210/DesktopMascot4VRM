using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
public class ProcessCaller : MonoBehaviour
{
	[SerializeField] string FileNameOnStart, ArgsOnStart;
	[SerializeField] bool CallOnStart = false, UseShellOnStart = false;
	void Start ()
	{
		if (CallOnStart)
		{
			CallProcess (FileNameOnStart, ArgsOnStart, UseShellOnStart);
		}
	}
	public void CallProcess (string FileName, string Args, bool UseShell)
	{
		ProcessStartInfo ProcInfo = new ProcessStartInfo ();
		ProcInfo.FileName = FileName;
		ProcInfo.Arguments = Args;
		ProcInfo.UseShellExecute = UseShell;
		Process.Start (ProcInfo);
	}
}