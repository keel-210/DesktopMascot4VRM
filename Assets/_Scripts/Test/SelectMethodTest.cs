using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMethodTest : MonoBehaviour
{
	[SerializeField] string Header;
	public void StringTest(string t)
	{
		Debug.Log(Header + t);
	}
	public void BoolTest(bool t)
	{
		Debug.Log(Header + t);
	}
	public void VectorTest(Vector3 t)
	{
		Debug.Log(Header + t);
	}
	public void GameObjectTest(GameObject t)
	{
		Debug.Log(Header + t.name);
	}
}