using UnityEngine;
using System.Collections;

public class DMMSimpleFollowCamera : MonoBehaviour {

	public Transform followTarget;
	public Vector3 offset;

	void Start() { }
	
	void Update ()
	{
		if (followTarget != null)
		{
			transform.position = new Vector3(followTarget.position.x + offset.x, followTarget.position.y + offset.y, followTarget.position.z + offset.z);
		}
	}
}
