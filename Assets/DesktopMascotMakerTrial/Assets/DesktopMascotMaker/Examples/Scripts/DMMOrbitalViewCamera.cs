using System.Collections;
using System.Windows.Forms;
using DesktopMascotMaker;
using UnityEngine;

[RequireComponent (typeof (Camera))]
[RequireComponent (typeof (MascotMaker))]
public class DMMOrbitalViewCamera : MonoBehaviour
{
	// If you want to use this script with MascotMakerMulti,
	// Uncomment the following line and replace 'MascotMaker.Instance' to 'mascotMakerMulti'.
	//public MascotMakerMulti mascotMakerMulti; // Assign MascotMakerMulti's instance to this variable.

	// Camera's target to look at
	public Transform target;

	// rotation speed
	public float speed = 0.3f;

	// vertical rotation limit
	public float yMinLimit = -60f;
	public float yMaxLimit = 80f;

	// for mascot's display size
	public float minSize = 0.88f; // minimum size of orthographic view. this parameter is for orthographic camera only.
	public float maxSize = 2.00f; // maximum size of orthographic view. this parameter is for orthographic camera only.
	public float nearDistance = 1.20f; // minimum distance between camera and target. this parameter is for perspective camera only. 
	public float farDistance = 2.00f; // maximum distance between camera and target. this parameter is for perspective camera only. 

	// for Rotate
	private bool isRotate = false;
	private int xPos0;
	private int yPos0;
	private float xRot = 0.0f;
	private float yRot = 0.0f;

	private Camera mascotCamera;
	[SerializeField]
	private float distance;

	void Start ()
	{
		if (target == null)
			Debug.LogError ("Desktop Mascot Maker error (DMMOrbitalViewCamera) : Target is not assined.", transform);

		mascotCamera = GetComponent<Camera> ();

		Vector3 angles = transform.eulerAngles;
		xRot = angles.y;
		yRot = angles.x;

		// Assign events to MascotMaker's EventHandler
		/* MascotMaker.Instance.OnRightMouseDown += RightMouseOn;
		MascotMaker.Instance.OnRightMouseUp += RightMouseUp;
		MascotMaker.Instance.OnMouseWheel += MouseWheel; */

		distance = (nearDistance + farDistance)/ 2.0f;
	}

	public void RightMouseOn (object sender, MouseEventArgs e)
	{
		xPos0 = System.Windows.Forms.Cursor.Position.X;
		yPos0 = System.Windows.Forms.Cursor.Position.Y;
		isRotate = true;
	}

	void RightMouseUp (object sender, MouseEventArgs e)
	{
		isRotate = false;
	}

	void MouseWheel (object sender, MouseEventArgs e)
	{
		float valTmp;

		if (mascotCamera.orthographic)
		{
			valTmp = mascotCamera.orthographicSize;
			valTmp -= ((float)e.Delta)/ (2400.0f);
			valTmp = Mathf.Clamp (valTmp, minSize, maxSize);
			mascotCamera.orthographicSize = valTmp;
		}
		else
		{
			valTmp = distance;
			valTmp -= ((float)e.Delta)/ (2400.0f);

			if (farDistance < nearDistance)// farDistance must be more than nearDistance.
				farDistance = nearDistance;

			distance = Mathf.Clamp (valTmp, nearDistance, farDistance);
		}
	}

	void Update ()//LateUpdate()
	{
		if (isRotate)
		{
			int xPosTmp = System.Windows.Forms.Cursor.Position.X;
			int yPosTmp = System.Windows.Forms.Cursor.Position.Y;

			xRot += (xPosTmp - xPos0)* speed;
			yRot += (yPosTmp - yPos0)* speed;
			xPos0 = xPosTmp;
			yPos0 = yPosTmp;

			yRot = ClampAngle (yRot, yMinLimit, yMaxLimit);
		}

		Quaternion rotation = Quaternion.Euler (yRot, xRot, 0);

		Vector3 distanceVector = new Vector3 (0.0f, 0.0f, -distance); //2);
		Vector3 position = rotation * distanceVector + target.position;

		transform.rotation = rotation;
		transform.position = position;
	}

	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}
}