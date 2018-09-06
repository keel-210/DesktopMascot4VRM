using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodCaller : MonoBehaviour
{
	[SerializeField] public MonoBehaviour objMono;
	[SerializeField, HideInInspector] public string CallbackName;
	public void CallMethod ()
	{
		if (objMono)
		{
			objMono.SendMessage (CallbackName);
		}
	}
}
class MethodTarget<Type>
{
	public Type target;
}