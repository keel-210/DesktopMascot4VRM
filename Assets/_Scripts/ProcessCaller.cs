using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
public class ProcessCaller : StateMachineBehaviour
{
	public ExecuteType exeType;
	public float DelayTime;
	[Multiline] public string FileName;
	[Multiline] public string Args;
	public bool UseShell = true;
	float Timer;
	bool Executed;

	public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (exeType == ExecuteType.Enter)
		{
			Call (FileName, Args, UseShell);
		}

	}
	public override void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (exeType == ExecuteType.Delay || exeType == ExecuteType.DelayStay)
		{
			if (Timer < DelayTime)
			{
				Timer += Time.deltaTime;
			}
			else
			{
				if (exeType == ExecuteType.DelayStay)
				{
					Call (FileName, Args, UseShell);
				}
				else if (!Executed)
				{
					Call (FileName, Args, UseShell);
				}
			}
		}
		if (exeType == ExecuteType.UpdateStay)
		{
			Call (FileName, Args, UseShell);
		}
	}
	public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (exeType == ExecuteType.Exit)
		{
			Call (FileName, Args, UseShell);
		}
	}
	public void Call (string FileName, string Args, bool UseShell)
	{
		ProcessStartInfo ProcInfo = new ProcessStartInfo ();
		ProcInfo.FileName = FileName;
		ProcInfo.Arguments = Args;
		ProcInfo.UseShellExecute = UseShell;
		Process proc = Process.Start (ProcInfo);
		proc.WaitForExit ();
	}
}