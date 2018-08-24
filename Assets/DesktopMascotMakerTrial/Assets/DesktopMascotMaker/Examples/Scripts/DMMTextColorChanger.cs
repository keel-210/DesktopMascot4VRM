using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
public class DMMTextColorChanger : MonoBehaviour {

	TextMesh textMesh;

	float timer = 0.0f;
	string defaultText;

	void Start ()
	{
		textMesh = gameObject.GetComponent<TextMesh>();
		defaultText = textMesh.text;
	}
	
	void Update ()
	{
		timer -= Time.deltaTime;

		if (timer < 0)
		{
			timer = 0;
			textMesh.text = defaultText;
		}

		textMesh.color = Color.Lerp(Color.black, Color.red, timer / 1.5f);
	}

	public void MakeTextRed(string value)
	{
		timer = 1.5f;

		textMesh.text = value;
		textMesh.color = Color.red;
	}
}
