using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(AnimatorConnector4Mono))]
public class AnimatorConnector4MonoEditor : Editor
{
	ReorderableList IntList, FloatList, BoolList, TriggerList;
	void OnEnable()
	{
		AnimatorConnector4Mono connector = target as AnimatorConnector4Mono;

		UnityEditor.Animations.AnimatorController AnimCtrl = null;
		if (connector.anim)
			AnimCtrl = connector.anim.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
		if (AnimCtrl)
		{
			List<string> IntNameList = new List<string>(), FloatNameList = new List<string>(), BoolNameList = new List<string>(), TriggerNameList = new List<string>();
			foreach (UnityEngine.AnimatorControllerParameter p in AnimCtrl.parameters)
			{
				switch (p.type)
				{

					case UnityEngine.AnimatorControllerParameterType.Int:
						IntNameList.Add(p.name);
						break;
					case UnityEngine.AnimatorControllerParameterType.Float:
						FloatNameList.Add(p.name);
						break;
					case UnityEngine.AnimatorControllerParameterType.Bool:
						BoolNameList.Add(p.name);
						break;
					case UnityEngine.AnimatorControllerParameterType.Trigger:
						TriggerNameList.Add(p.name);
						break;
					default:
						break;
				}
			}
			string[] IntNames = IntNameList.ToArray();
			string[] FloatNames = FloatNameList.ToArray();
			string[] BoolNames = BoolNameList.ToArray();
			string[] TriggerNames = TriggerNameList.ToArray();

			//Integer Parameters Reorderable List
			if (IntNames.Length > 0)
			{
				var IProp = serializedObject.FindProperty("IntParams");
				IntList = new ReorderableList(serializedObject, IProp);
				IntList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Integer Parameters");
				IntList.drawElementCallback = (rect, index, isActive, isFocused) =>
				{
					foreach (IntParam i in connector.IntParams)
					{
						var nameRect = new Rect(rect) { width = 96, };
						var valueRect = new Rect(rect) { width = rect.width - 128, height = rect.height - 4, x = rect.x + 128 };
						if (i.name != "")
						{
							var selectedIndex = IntNameList.FindIndex(x => x.Equals(i.name));
							selectedIndex = EditorGUI.Popup(nameRect, selectedIndex, IntNames);
							i.name = IntNames[selectedIndex];
						}
						else
						{
							i.name = IntNames[0];
						}
						i.value = EditorGUI.IntField(valueRect, i.value);
					}
				};
			}
			//Float Parameters Reorderable List
			if (FloatNames.Length > 0)
			{
				var FProp = serializedObject.FindProperty("FloatParams");
				FloatList = new ReorderableList(serializedObject, FProp);
				FloatList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Float Parameters");
				FloatList.drawElementCallback = (rect, index, isActive, isFocused) =>
				{
					foreach (FloatParam i in connector.FloatParams)
					{
						var nameRect = new Rect(rect) { width = 96, };
						var valueRect = new Rect(rect) { width = rect.width - 128, height = rect.height - 4, x = rect.x + 128 };
						if (i.name != "")
						{
							var selectedIndex = FloatNameList.FindIndex(x => x.Equals(i.name));
							selectedIndex = EditorGUI.Popup(nameRect, selectedIndex, FloatNames);
							i.name = FloatNames[selectedIndex];
						}
						else
						{
							i.name = FloatNames[0];
						}
						i.value = EditorGUI.FloatField(valueRect, i.value);
					}
				};
			}
			//Boolean Parameters Reorderable List
			if (BoolNames.Length > 0)
			{
				var BProp = serializedObject.FindProperty("BoolParams");
				BoolList = new ReorderableList(serializedObject, BProp);
				BoolList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Bool Parameters");
				BoolList.drawElementCallback = (rect, index, isActive, isFocused) =>
				{
					foreach (BoolParam i in connector.BoolParams)
					{
						var nameRect = new Rect(rect) { width = 96, };
						var valueRect = new Rect(rect) { width = rect.width - 128, height = rect.height - 4, x = rect.x + 128 };
						if (i.name != "")
						{
							var selectedIndex = BoolNameList.FindIndex(x => x.Equals(i.name));
							selectedIndex = EditorGUI.Popup(nameRect, selectedIndex, BoolNames);
							i.name = BoolNames[selectedIndex];
						}
						else
						{
							i.name = BoolNames[0];
						}
						i.value = EditorGUI.Toggle(valueRect, i.value);
					}
				};
			}
			//Trigger Parameters Reorderable List
			if (TriggerNames.Length > 0)
			{
				var TProp = serializedObject.FindProperty("TriggerParams");
				TriggerList = new ReorderableList(serializedObject, TProp);
				TriggerList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Trigger Parameters");
				TriggerList.drawElementCallback = (rect, index, isActive, isFocused) =>
				{
					foreach (TriggerParam i in connector.TriggerParams)
					{
						var nameRect = new Rect(rect) { width = 96, };
						var valueRect = new Rect(rect) { width = rect.width - 128, x = rect.x + 128 };
						if (i.name != "")
						{
							var selectedIndex = TriggerNameList.FindIndex(x => x.Equals(i.name));
							selectedIndex = EditorGUI.Popup(nameRect, selectedIndex, TriggerNames);
							i.name = TriggerNames[selectedIndex];
						}
						else
						{
							i.name = TriggerNames[0];
						}
					}
				};
			}
		}
	}
	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		var anim = serializedObject.FindProperty("anim");
		EditorGUILayout.PropertyField(anim);

		if (IntList != null)
			IntList.DoLayoutList();
		if (FloatList != null)
			FloatList.DoLayoutList();
		if (BoolList != null)
			BoolList.DoLayoutList();
		if (TriggerList != null)
			TriggerList.DoLayoutList();

		serializedObject.ApplyModifiedProperties();
	}
}