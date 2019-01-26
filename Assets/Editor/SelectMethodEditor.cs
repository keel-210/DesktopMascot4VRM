using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SelectMethod))]
public class SelectMethodEditor : Editor
{

	SelectMethod m_Target = null;
	MonoBehaviour m_PreviousObject = null;
	List<string> m_Methods = new List<string>();

	void ClearMethods()
	{
		m_Methods = new List<string>();
		m_Target.CallbackName = "";
		m_Target.MethodArgument = null;
	}

	void CollectMethods()
	{
		if (m_Target == null || !m_Target.TargetObject)
		{
			ClearMethods();
			return;
		}
		if (m_PreviousObject == m_Target.TargetObject)
		{
			return;
		}
		m_PreviousObject = m_Target.TargetObject;

		List<string> result = new List<string>();
		result.Add("None");
		var methods = m_Target.TargetObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public)
			.Where(x => x.DeclaringType == m_Target.TargetObject.GetType())
			.Where(x => x.GetParameters().Length <= 1)
			.Select(x => x.Name)
			.ToArray();
		result.AddRange(methods);
		m_Methods = result;
	}

	void OnEnable()
	{
		// m_Target = (SelectMethod)target;
		CollectMethods();
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		CollectMethods();

		if (m_Target.TargetObject == null)
		{
			return;
		}

		if (m_Methods.Count == 0)
		{
			return;
		}
		int index = m_Methods.FindIndex(item => item == m_Target.CallbackName);
		if (index == -1)
		{
			index = 0;
		}
		using(new EditorGUILayout.HorizontalScope())
		{
			EditorGUILayout.LabelField("Methods");
			m_Target.CallbackName = m_Methods[EditorGUILayout.Popup(index, m_Methods.ToArray())];

			if (m_Target.CallbackName == "None")
				return;
		}
		var arg = m_Target.TargetObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public)
			.Where(x => x.Name == m_Target.CallbackName).First();
		if (arg.GetParameters().Length == 0)
			return;
		arg.GetParameters();
		if (arg.GetParameters()[0].ParameterType == typeof(string))
			EditorGUILayout.TextField((string)m_Target.MethodArgument);
	}
}