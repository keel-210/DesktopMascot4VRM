using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	[SerializeField] public GameObject menuObj;
	[SerializeField] public bool UseRot, UseScale, UseAlpha;
	[SerializeField] public float RotAngle, RotTime, ScaleTime, AlphaTime;
	LayerMask mask;
	bool IsOpen;
	void Start ()
	{
		menuObj.SetActive (false);
		mask = LayerMask.NameToLayer ("UI");
	}
	void Update ()
	{
		if (Input.GetMouseButtonDown (1))
		{
			if (IsOpen)
			{
				MenuClose ();
			}
			else
			{
				MenuOpen ();
			}
		}
		if (Input.GetMouseButtonDown (0)&& IsOpen)
		{
			PointerEventData pointer = new PointerEventData (EventSystem.current);
			pointer.position = Input.mousePosition;
			List<RaycastResult> result = new List<RaycastResult> ();
			EventSystem.current.RaycastAll (pointer, result);
			if (result.Count == 0)
			{
				MenuClose ();
			}
		}
	}
	void MenuOpen ()
	{
		if (UseRot)
		{
			var rot = menuObj.AddComponent<RectRotator> ();
			rot.Init (Vector3.zero, Vector3.forward * RotAngle, 0, RotTime);
		}
		if (UseScale)
		{
			var scale = menuObj.AddComponent<RectScaler> ();
			scale.Init (Vector3.zero, Vector3.one, 0, ScaleTime);
		}
		if (UseAlpha)
		{
			var alpha = menuObj.AddComponent<AlphaController> ();
			alpha.Init (new Color (0, 0, 0, 1), AlphaTime, 0, false);
		}
		menuObj.SetActive (true);
		IsOpen = true;
	}
	public void MenuClose ()
	{
		if (UseRot)
		{
			var rot = menuObj.AddComponent<RectRotator> ();
			rot.Init (Vector3.forward * RotAngle, Vector3.zero, 0, RotTime);
		}
		if (UseScale)
		{
			var scale = menuObj.AddComponent<RectScaler> ();
			scale.Init (Vector3.one, Vector3.zero, 0, ScaleTime);
		}
		if (UseAlpha)
		{
			var alpha = menuObj.AddComponent<AlphaController> ();
			alpha.Init (new Color (0, 0, 0, 1), AlphaTime, 0, true);
		}
		StartCoroutine (this.DelayMethod (0.26f, ()=>
		{
			menuObj.SetActive (false);
			IsOpen = false;
		}));
	}
}