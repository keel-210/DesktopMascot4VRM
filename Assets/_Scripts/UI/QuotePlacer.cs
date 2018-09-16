using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuotePlacer : StateMachineBehaviour
{
	public ExecuteType exeType;
	public float DelayTime;
	[Multiline] public string QuoteString;
	public Color PanelColor = Color.white, TextColor = Color.black;
	public HumanBodyBones PlaceBone = HumanBodyBones.Head;
	public Vector2 PlaceOffset = new Vector2 (-100, 0), PanelCollar = new Vector2 (10, 10);
	public AnchorPresets anchor = AnchorPresets.MiddleCenter;
	float Timer;
	bool Executed;
	Quote placer;
	public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (exeType == ExecuteType.Enter)
		{
			QuotePlace (animator);
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
					QuotePlace (animator);
				}
				else if (!Executed)
				{
					QuotePlace (animator);
				}
			}
		}
		if (exeType == ExecuteType.UpdateStay)
		{
			QuotePlace (animator);
		}
	}
	public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (exeType == ExecuteType.Exit)
		{
			QuotePlace (animator);
		}
	}
	void QuotePlace (Animator animator)
	{
		if (placer == null)
		{
			placer = FindObjectOfType<Quote> ();
		}
		placer.MakeQuote (animator, QuoteString, PanelColor, TextColor, PlaceBone, PlaceOffset, PanelCollar, anchor);
		Executed = true;
	}
}