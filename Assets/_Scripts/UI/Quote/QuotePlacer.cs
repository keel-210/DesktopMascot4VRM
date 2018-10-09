using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuotePlacer : StateMachineBehaviour
{
	public ExecuteType exeType;
	public float DelayTime;
	public AllStyle allStyle;
	[TextArea(5, 100)] public string QuoteString;
	public Color PanelColor = Color.white, TextColor = Color.black;
	public HumanBodyBones PlaceBone = HumanBodyBones.Head;
	public Vector2 PlaceOffset = new Vector2(-100, 0), PanelCollar = new Vector2(10, 10);
	public AnchorPresets anchor = AnchorPresets.MiddleCenter;
	public PivotPresets pivot = PivotPresets.MiddleCenter;
	public QuoteStyle quoteStyle;
	public int ListMax;
	public AlphaStyle alphaStyle;
	public float AlphaChangingTime;
	public Vector3 moveDirection;
	public float MovingTime;
	public BackImage backImage;
	public int FontSize;
	public Font font;
	float Timer;
	bool Executed;
	Quote quote;
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (exeType == ExecuteType.Enter)
		{
			QuotePlace(animator);
		}
	}
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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
					QuotePlace(animator);
				}
				else if (!Executed)
				{
					QuotePlace(animator);
				}
			}
		}
		if (exeType == ExecuteType.UpdateStay)
		{
			QuotePlace(animator);
		}
	}
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (exeType == ExecuteType.Exit)
		{
			QuotePlace(animator);
		}
	}
	void QuotePlace(Animator animator)
	{
		Executed = true;
		if (quote == null)
		{
			quote = FindObjectOfType<Quote>();
		}
		switch (allStyle)
		{
			case AllStyle.NormalQuote:
				quote.Settings(out PanelColor, out TextColor, out PlaceBone,
					out PlaceOffset, out PanelCollar, out anchor, out pivot, out quoteStyle, out ListMax, out alphaStyle,
					out AlphaChangingTime, out moveDirection, out MovingTime, out backImage, out FontSize, out font);
				quote.MakeQuote(animator, QuoteString, PanelColor, TextColor, PlaceBone,
					PlaceOffset, PanelCollar, anchor, pivot, QuoteStyle.OnlyOne, ListMax, AlphaStyle.AlphaAlpha,
					backImage, AlphaChangingTime, moveDirection, MovingTime, FontSize, font);
				break;
			case AllStyle.NormalList:
				quote.Settings(out PanelColor, out TextColor, out PlaceBone,
					out PlaceOffset, out PanelCollar, out anchor, out pivot, out quoteStyle, out ListMax, out alphaStyle,
					out AlphaChangingTime, out moveDirection, out MovingTime, out backImage, out FontSize, out font);
				quote.MakeQuote(animator, QuoteString, PanelColor, TextColor, PlaceBone,
					PlaceOffset, PanelCollar, anchor, pivot, QuoteStyle.List, ListMax, AlphaStyle.AlphaAlpha,
					backImage, AlphaChangingTime, moveDirection, MovingTime, FontSize, font);
				break;
			case AllStyle.CustomSetting:
				quote.Settings(out PanelColor, out TextColor, out PlaceBone,
					out PlaceOffset, out PanelCollar, out anchor, out pivot, out quoteStyle, out ListMax, out alphaStyle,
					out AlphaChangingTime, out moveDirection, out MovingTime, out backImage, out FontSize, out font);
				quote.MakeQuote(animator, QuoteString, PanelColor, TextColor, PlaceBone,
					PlaceOffset, PanelCollar, anchor, pivot, quoteStyle, ListMax, alphaStyle, backImage, AlphaChangingTime,
					moveDirection, MovingTime, FontSize, font);
				break;
			case AllStyle.Free:
				quote.MakeQuote(animator, QuoteString, PanelColor, TextColor, PlaceBone,
					PlaceOffset, PanelCollar, anchor, pivot, quoteStyle, ListMax, alphaStyle, backImage, AlphaChangingTime,
					moveDirection, MovingTime, FontSize, font);
				break;
		}
	}
}