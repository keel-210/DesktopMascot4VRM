using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomPropertyDrawer(typeof(SelectMethod))]
public class SelectMethodDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		using(new EditorGUI.PropertyScope(position, label, property))
		{}
	}
}