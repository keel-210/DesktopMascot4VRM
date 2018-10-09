using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(QuotePlacer))]
public class QuotePlacerEditor : Editor
{
	QuotePlacer placer;
	bool _isOpen = false;
	string[] props = {
		"PanelColor",
		"TextColor",
		"PlaceBone",
		"PlaceOffset",
		"PanelCollar",
		"anchor",
		"pivot",
		"quoteStyle",
		"ListMax",
		"alphaStyle",
		"AlphaChangingTime",
		"moveDirection",
		"MovingTime",
		"backImage",
		"FontSize",
		"font"
	};
	void OnEnable()
	{
		placer = target as QuotePlacer;
	}
	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		DisplayProperty("exeType");
		if (placer.exeType == ExecuteType.Delay || placer.exeType == ExecuteType.DelayStay)
		{
			DisplayProperty("DelayTime");
		}
		else
		{
			EditorGUI.BeginDisabledGroup(true);
			DisplayProperty("DelayTime");
			EditorGUI.EndDisabledGroup();
		}
		DisplayProperty("allStyle");
		DisplayProperty("QuoteString");

		bool isOpen = EditorGUILayout.Foldout(_isOpen, "Paramaters");
		if (_isOpen != isOpen)
		{
			_isOpen = isOpen;
		}
		if (isOpen)
		{
			foreach (string s in props)
			{
				DisplayProperty(s);
			}
		}
		serializedObject.ApplyModifiedProperties();
	}
	void DisplayProperty(string Name)
	{
		var obj = serializedObject.FindProperty(Name);
		EditorGUILayout.PropertyField(obj);
	}
}