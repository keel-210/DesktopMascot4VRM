using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(VoiceRecogCommand))]
public class TutorialMessagesDrawer : Editor
{
	private ReorderableList RL;
	private void OnEnable()
	{
		var ListProp = serializedObject.FindProperty("CommandList");
		var recog = (VoiceRecogCommand)target;
		RL = new ReorderableList(recog.CommandList, typeof(SelectMethod));
		RL.elementHeight = EditorGUIUtility.singleLineHeight * 3 + 6;
		RL.drawHeaderCallback = (rect) =>
		{
			EditorGUI.LabelField(rect, "OnTest()");
		};
		RL.drawElementCallback = (rect, index, isActive, isFocused) =>
		{
			serializedObject.Update();
			var element = ListProp.GetArrayElementAtIndex(index);
			var c = recog.CommandList[index];
			var m = c.Method;
			Debug.Log(element.serializedObject.FindProperty("Method"));
			Debug.Log(m);
			if (m == null)
			{
				Debug.Log("Null fix");
				c.Method = new SelectMethod();
			}
			var KeyRect = new Rect(rect)
			{
				width = rect.width / 2,
					height = EditorGUIUtility.singleLineHeight,
					x = rect.width / 2
			};
			var MonoRect = new Rect(rect)
			{
				height = EditorGUIUtility.singleLineHeight,
					y = KeyRect.y + EditorGUIUtility.singleLineHeight + 2
			};
			var MethodRect = new Rect(rect)
			{
				height = EditorGUIUtility.singleLineHeight,
					width = rect.width / 3,
					y = MonoRect.y + EditorGUIUtility.singleLineHeight + 2
			};
			var ArgumentRect = new Rect(rect)
			{
				height = EditorGUIUtility.singleLineHeight,
					width = (rect.width * 2 / 3) - 4,
					x = MethodRect.x + MethodRect.width + 2,
					y = MonoRect.y + EditorGUIUtility.singleLineHeight + 2
			};
			c.Key = EditorGUI.TextField(KeyRect, c.Key);
			m.TargetObject = (MonoBehaviour)EditorGUI.ObjectField(MonoRect, m.TargetObject, typeof(MonoBehaviour), true);
			var methods = CollectMethods(m);
			if (methods != null)
			{
				int NameIndex = methods.FindIndex(item => item == m.CallbackName);
				NameIndex = NameIndex != -1 ? NameIndex : 0;

				EditorGUI.LabelField(MethodRect, "Methods");
				m.CallbackName = methods[EditorGUI.Popup(MethodRect, NameIndex, methods.ToArray())];
			}
			if (m.TargetObject)
			{
				var arg = m.TargetObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public)
					.Where(x => x.Name == m.CallbackName);
				if (arg.Count() != 0)
				{
					var param = arg.First();
					if (param.GetParameters().Length != 0)
					{
						param.GetParameters();
						if (param.GetParameters()[0].ParameterType == typeof(string))
						{
							if (m.MethodArgument == null)
								m.MethodArgument = "";
							m.MethodArgument = EditorGUI.TextField(ArgumentRect, (string)m.MethodArgument);
						}
						else if (param.GetParameters()[0].ParameterType == typeof(bool))
						{
							if (m.MethodArgument == null)
								m.MethodArgument = false;
							m.MethodArgument = EditorGUI.Toggle(ArgumentRect, (bool)m.MethodArgument);
						}
						else if (param.GetParameters()[0].ParameterType == typeof(int))
						{
							if (m.MethodArgument == null)
								m.MethodArgument = (int)0;
							m.MethodArgument = EditorGUI.IntField(ArgumentRect, (int)m.MethodArgument);
						}
						else if (param.GetParameters()[0].ParameterType == typeof(float))
						{
							if (m.MethodArgument == null)
								m.MethodArgument = 0f;
							m.MethodArgument = EditorGUI.FloatField(ArgumentRect, (float)m.MethodArgument);
						}
						else if (param.GetParameters()[0].ParameterType == typeof(double))
						{
							if (m.MethodArgument == null)
								m.MethodArgument = 0.0;
							m.MethodArgument = EditorGUI.DoubleField(ArgumentRect, (double)m.MethodArgument);
						}
						else
						{
							m.ArgumentObject = EditorGUI.ObjectField(ArgumentRect, m.ArgumentObject, param.GetParameters()[0].ParameterType, true);
						}
					}
				}
				serializedObject.ApplyModifiedProperties();
			}
			EditorGUI.PropertyField(rect, element);
		};
	}
#endif
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		serializedObject.Update();
		RL.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
	}

	List<string> CollectMethods(SelectMethod m)
	{
		if (m == null || !m.TargetObject)
		{
			return null;
		}

		List<string> result = new List<string>();
		result.Add("None");
		var methods = m.TargetObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public)
			.Where(x => x.DeclaringType == m.TargetObject.GetType())
			.Where(x => x.GetParameters().Length <= 1)
			.Select(x => x.Name)
			.ToArray();
		result.AddRange(methods);
		return result;
	}
}