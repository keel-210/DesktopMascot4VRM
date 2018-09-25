using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (AnimatorConnector4Mono))]
[RequireComponent (typeof (RectTransform))]
[RequireComponent (typeof (CanvasRenderer))]
[RequireComponent (typeof (Image))]
[RequireComponent (typeof (Button))]

public class Menu : MonoBehaviour
{
	[SerializeField] AnimatorConnector4Mono connector;
#if UNITY_EDITOR
	Button but;
	void Reset ()
	{
		but = GetComponent<Button> ();
		connector = GetComponent<AnimatorConnector4Mono> ();
		UnityEditor.Events.UnityEventTools.RemovePersistentListener<GameObject> (but.onClick, OnClick);
		UnityEditor.Events.UnityEventTools.AddObjectPersistentListener<GameObject> (but.onClick, OnClick, gameObject);
	}
#endif
	public void OnClick (GameObject obj)
	{
		connector.SetAnimCtrlParams ();
	}
}