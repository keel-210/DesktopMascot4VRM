using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIMover : MonoBehaviour
{
	public bool IsMoving;
	[SerializeField] RectTransform rect;
	[SerializeField] float MovingTime;
	[SerializeField] Vector2 StartPos, EndPos;
	float Speed;
	void Start ()
	{
		Speed = (EndPos - StartPos).magnitude / MovingTime;
	}
	public void MoveUI ()
	{
		if (!IsMoving)
		{
			IsMoving = true;
			StartCoroutine (this.Move ());
		}
	}
	IEnumerator Move ()
	{
		float startTime = Time.time;
		while ((Time.time - startTime)< MovingTime)
		{
			rect.anchoredPosition = Vector2.MoveTowards (rect.anchoredPosition, EndPos, Speed * Time.deltaTime);
			yield return null;
		}
		rect.anchoredPosition = EndPos;
		Vector3 p = StartPos;
		StartPos = EndPos;
		EndPos = p;
		IsMoving = false;

	}
}