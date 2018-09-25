using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorConnector4Mono : MonoBehaviour
{
	public Animator anim = new Animator ();
	public List<IntParam> IntParams = new List<IntParam> ();
	public List<FloatParam> FloatParams = new List<FloatParam> ();
	public List<BoolParam> BoolParams = new List<BoolParam> ();
	public List<TriggerParam> TriggerParams = new List<TriggerParam> ();
	float Timer;
	public void SetAnimCtrlParams ()
	{
		foreach (IntParam i in IntParams)
		{
			anim.SetInteger (i.name, i.value);
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
	}
}