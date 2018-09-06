using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer (typeof (DirectoryPathAttribute))]
public class DirectoryPathDrawer : PropertyDrawer
{

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		if (property.propertyType != SerializedPropertyType.String)
		{
			EditorGUI.HelpBox (position, "This attribute is a string only.", MessageType.Error);
			return;
		}

		EditorGUI.BeginProperty (position, label, property);

		Rect buttonPosition = new Rect (position.x + position.width - 30, position.y, 30, position.height);
		Rect textPosition = new Rect (position.x, position.y, position.width - buttonPosition.width - 5, position.height);
		EditorGUI.TextField (textPosition, label, property.stringValue);

		if (GUI.Button (buttonPosition, "..."))
		{
			string dirpath = "";
			// ファイルが存在していたら、そのディレクトリを開く
			if (System.IO.File.Exists (property.stringValue))
			{
				dirpath = System.IO.Path.GetDirectoryName (property.stringValue);
			}

			DirectoryPathAttribute filepath = (DirectoryPathAttribute)attribute;
			string path = EditorUtility.OpenFolderPanel ("select file", dirpath, filepath.extensionFilter);
			if (false == string.IsNullOrEmpty (path))
			{
				// ファイルが選択されていたらそれを反映させる
				property.stringValue = path;
			}
		}

		EditorGUI.EndProperty ();

	}

}