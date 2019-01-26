using System;
using UnityEngine;
using UnityEngine.EventSystems;
[System.Serializable]
public class SelectMethod : ISerializationCallbackReceiver
{
	public MonoBehaviour TargetObject;

	public string CallbackName;

	public System.Object MethodArgument;
	[SerializeField, HideInInspector] string ArgSerialized;
	[SerializeField, HideInInspector] string ArgType;

	public void OnBeforeSerialize()
	{
		if (MethodArgument != null)
		{
			ArgSerialized = MethodArgument.ToString();
			ArgType = MethodArgument.GetType().ToString();
		}
	}
	public void OnAfterDeserialize()
	{
		if (!string.IsNullOrEmpty(ArgSerialized) && !string.IsNullOrEmpty(ArgType))
		{
			DeserializeFromString();
		}
	}
	public void Execute()
	{
		if (!string.IsNullOrEmpty(ArgSerialized) && !string.IsNullOrEmpty(ArgType))
		{
			DeserializeFromString();
		}
		if (MethodArgument != null)
		{
			TargetObject.SendMessage(CallbackName, MethodArgument);
		}
		else
		{
			TargetObject.SendMessage(CallbackName);
		}
	}
	void DeserializeFromString()
	{
		if (string.IsNullOrEmpty(ArgSerialized) || string.IsNullOrEmpty(ArgType))
		{
			return;
		}
		var type = System.Type.GetType(ArgType);
		if (type == null)
			type = System.Reflection.Assembly.Load("UnityEngine.dll").GetType(ArgType);
		if (type == typeof(string))
			MethodArgument = ArgSerialized;
		if (type == typeof(bool))
			MethodArgument = bool.Parse(ArgSerialized);
		else if (type == typeof(int))
			MethodArgument = int.Parse(ArgSerialized);
		else if (type == typeof(float))
			MethodArgument = float.Parse(ArgSerialized);
		else if (type == typeof(double))
			MethodArgument = double.Parse(ArgSerialized);
		else if (type == typeof(Vector2))
			MethodArgument = Vector2FromString(ArgSerialized);
		else if (type == typeof(Vector3))
			MethodArgument = Vector3FromString(ArgSerialized);
		else if (type == typeof(Vector4))
			MethodArgument = Vector4FromString(ArgSerialized);
	}
	Vector2 Vector2FromString(string s)
	{
		string[] sArray = s.Substring(1, s.Length - 2).Split(',');
		return new Vector2(float.Parse(sArray[0]), float.Parse(sArray[1]));
	}
	Vector3 Vector3FromString(string s)
	{
		string[] sArray = s.Substring(1, s.Length - 2).Split(',');
		return new Vector3(float.Parse(sArray[0]), float.Parse(sArray[1]), float.Parse(sArray[2]));
	}
	Vector4 Vector4FromString(string s)
	{
		string[] sArray = s.Substring(1, s.Length - 2).Split(',');
		return new Vector4(float.Parse(sArray[0]), float.Parse(sArray[1]), float.Parse(sArray[2]), float.Parse(sArray[3]));
	}
}