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
        Debug.Log(EyePos);
		Quaternion rot = Quaternion.LookRotation(OVO.Position,Vector3.up);
		OVO.Rotation = new Vector3(0,-rot.eulerAngles.y,0);
	}
}