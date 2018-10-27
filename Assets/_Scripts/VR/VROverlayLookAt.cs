using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VROverlayLookAt : MonoBehaviour
{
	[SerializeField] EasyOpenVROverlayForUnity OVO;

	void Update()
	{
		Vector3 EyePos = InputTracking.GetLocalPosition(XRNode.CenterEye);
		Quaternion rot = Quaternion.LookRotation(OVO.Position - EyePos);
		OVO.Rotation = rot.eulerAngles;
	}
}