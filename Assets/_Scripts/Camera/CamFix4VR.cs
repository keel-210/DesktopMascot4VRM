using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CamFix4VR : MonoBehaviour
{
    [SerializeField]Vector3 basePos;

    void Update()
    {
        // VR.InputTracking から hmd の位置を取得
        Vector3 trackingPos =InputTracking.GetLocalPosition(XRNode.CenterEye);
        Quaternion trackingRot = InputTracking.GetLocalRotation(XRNode.CenterEye);

        // CameraController 自体の rotation が
        // zero でなければ rotation を掛ける
        // trackingPosition = trackingPos * transform.rotation;

        // 固定したい位置から hmd の位置を
        // 差し引いて実質 hmd の移動を無効化する
        Debug.Log(trackingPos);
        //transform.position = basePos + trackingPos;
        var diffRot = Quaternion.Inverse(trackingRot);
        //transform.rotation = transform.rotation * diffRot;
    }
}
