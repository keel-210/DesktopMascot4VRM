using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomPropertyDrawer(typeof(SelectMethod))]
public class SelectMethodDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		using(new EditorGUI.PropertyScope(position, label, property))
		{

		}
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