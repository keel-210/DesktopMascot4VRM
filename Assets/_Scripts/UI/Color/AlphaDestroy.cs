using UnityEngine;

public class AlphaDestroy : MonoBehaviour
{
	CanvasRenderer renderer;
	void Start ()
	{
		renderer = GetComponent<CanvasRenderer> ();
	}
	void Update ()
	{
		if (renderer.GetAlpha ()<= 0)
		{
			Destroy (gameObject);
		}
	}
}