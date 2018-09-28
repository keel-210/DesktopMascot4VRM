using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Activater : MonoBehaviour
{
	public List<GameObject> list = new List<GameObject> ();
	public void Activate ()
	{
		list.ForEach (item => item.SetActive (true));
	}
	public void DeActivate ()
	{
		list.ForEach (item => item.SetActive (false));
	}
	public void ActiveInverce ()
	{
		list.ForEach (item => item.SetActive (!item.activeInHierarchy));
	}
}