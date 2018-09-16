using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimatorConnecter : StateMachineBehaviour
{
	public Animator anim = new Animator ();
	public ExecuteType exeType;
	public float DelayTime;
	public List<IntParam> IntParams = new List<IntParam> ();
	public List<FloatParam> FloatParams = new List<FloatParam> ();
	public List<BoolParam> BoolParams = new List<BoolParam> ();
	public List<TriggerParam> TriggerParams = new List<TriggerParam> ();
	float Timer;
	bool Executed;
	public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (exeType == ExecuteType.Enter)
		{
			SetAnimCtrlParams ();
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
					SetAnimCtrlParams ();
				}
				else if (!Executed)
				{
					SetAnimCtrlParams ();
				}
			}
		}
		if (exeType == ExecuteType.UpdateStay)
		{
			SetAnimCtrlParams ();
		}
	}
	public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (exeType == ExecuteType.Exit)
		{
			SetAnimCtrlParams ();
		}
	}
	void SetAnimCtrlParams ()
	{
		foreach (IntParam i in IntParams)
		{
			anim.SetInteger (i.name, i.value);
			Debug.Log (i.name + i.value);
		}
		foreach (FloatParam f in FloatParams)
		{
			anim.SetFloat (f.name, f.value);
		}
		foreach (BoolParam b in BoolParams)
		{
			anim.SetBool (b.name, b.value);
		}
		foreach (TriggerParam t in TriggerParams)
		{
			anim.SetTrigger (t.name);
		}
		Executed = true;
	}

}
public enum ExecuteType
{
	Enter,
	Exit,
	Delay,
	UpdateStay,
	DelayStay,
}

[Serializable]
public class IntParam
{
	public string name;
	public int value;
	public int hash;
}

[Serializable]
public class FloatParam
{
	public string name;
	public float value;
	public int hash;
}

[Serializable]
public class BoolParam
{
	public string name;
	public bool value;
	public int hash;
}

[Serializable]
public class TriggerParam
{
	public string name;
	public int hash;
}