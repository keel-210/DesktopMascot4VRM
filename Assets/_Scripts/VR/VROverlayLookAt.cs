using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VROverlayLookAt : MonoBehaviour
{
	[SerializeField] EasyOpenVROverlayForUnity OVO;
	void Update()
	{
		Vector3 HeadPos = InputTracking.GetLocalPosition(XRNode.Head);
		Vector3 CamPos = Camera.main.transform.position;
		Quaternion rot = Quaternion.LookRotation(OVO.Position, Vector3.up);
		OVO.Rotation = -rot.eulerAngles;
	}
}