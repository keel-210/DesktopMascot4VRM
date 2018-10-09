using UnityEngine;

public class Camera4Wheel : MonoBehaviour
{
	private WindowController windowController;
	private CameraController cameraController;
	private CameraController.WheelMode originalWheelMode;
	void Start()
	{
		if (!cameraController)
		{
			cameraController = FindObjectOfType<CameraController>();
			if (cameraController)
			{
				originalWheelMode = cameraController.wheelMode;
			}
		}
		windowController = FindObjectOfType<WindowController>();
	}

	void Update()
	{
		// ホイール操作は不透明なところでのみ受け付けさせる
		if (windowController && cameraController)
		{
			Vector2 pos = Input.mousePosition;
			bool inScreen = (pos.x >= 0 && pos.x < Screen.width && pos.y >= 0 && pos.y < Screen.height);
			if (windowController.isFocusable && inScreen)
			{
				cameraController.wheelMode = originalWheelMode;
			}
			else
			{
				cameraController.wheelMode = CameraController.WheelMode.None;
			}
		}
	}
}