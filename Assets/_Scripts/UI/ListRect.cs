using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListRect : MonoBehaviour
{
	public List<RectTransform> list = new List<RectTransform> ();
	public FixedStandard fixStandard;
	public Vector3 ListDirection;
	RectTransform fixedRect;
	void Update ()
	{
		list = list.Where (item => item != null).ToList ();
		if (fixStandard == FixedStandard.Top)
		{
			fixedRect = list[0];
			for (int i = 1; i < list.Count; i++)
			{
				list[i].position = list[i - 1].position +
					new Vector3 (ListDirection.x * list[i - 1].sizeDelta.x / 2, ListDirection.y * list[i - 1].sizeDelta.y / 2, 0);
			}
		}
		else
		{
			fixedRect = list[list.Count - 1];
			for (int i = list.Count - 2; i >= 0; i--)
			{
				list[i].position = list[i + 1].position +
					new Vector3 (ListDirection.x * list[i + 1].sizeDelta.x / 2, ListDirection.y * list[i + 1].sizeDelta.y / 2, 0);
			}
		}
	}
	public enum FixedStandard
	{
		Top,
		Last
	}
}