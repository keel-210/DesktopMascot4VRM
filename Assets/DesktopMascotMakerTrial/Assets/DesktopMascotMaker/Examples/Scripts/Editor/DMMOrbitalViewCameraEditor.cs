using UnityEngine;
using UnityEditor;
using DesktopMascotMaker;

[CustomEditor(typeof(DMMOrbitalViewCamera))]
public class DMMOrbitalViewCameraEditor : Editor
{
    DMMOrbitalViewCamera orbitalViewCamera;

    GUIContent gcTarget = new GUIContent("Camera Target", "Camera's target to look at.");
    GUIContent gcSpeed = new GUIContent("Rotation Speed", "Rotation speed of orbital view.");
    GUIContent gcYMinLimit = new GUIContent("Rotation Limit Min", "Minimum limitation of vertical rotation.");
    GUIContent gcYMaxLimit = new GUIContent("Rotation Limit Max", "Maximum limitation of vertical rotation.");
    GUIContent gcMinSize = new GUIContent("Min Size", "Minimum limitation of the orthographic camera's size.");
    GUIContent gcMaxSize = new GUIContent("Max Size", "Maximum limitation of the orthographic camera's size.");
    GUIContent gcNearDistance = new GUIContent("Near Distance", "Minimum limitation of the ditance between camera and camera's target. This parameter is used with perspective camera.");
    GUIContent gcFarDistance = new GUIContent("Far Distance", "Maximum limitation of the ditance between camera and camera's target. This parameter is used with perspective camera.");

    void OnEnable()
    {
        orbitalViewCamera = (DMMOrbitalViewCamera)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        orbitalViewCamera.target = EditorGUILayout.ObjectField(gcTarget, orbitalViewCamera.target, typeof(Transform), true) as Transform;
        orbitalViewCamera.speed = EditorGUILayout.FloatField(gcSpeed, orbitalViewCamera.speed);
        orbitalViewCamera.yMinLimit = EditorGUILayout.FloatField(gcYMinLimit, orbitalViewCamera.yMinLimit);
        float yMaxLimitTmp = EditorGUILayout.FloatField(gcYMaxLimit, orbitalViewCamera.yMaxLimit);
        
        if (yMaxLimitTmp < orbitalViewCamera.yMinLimit)
            yMaxLimitTmp = orbitalViewCamera.yMinLimit;

        orbitalViewCamera.yMaxLimit = yMaxLimitTmp;

        if (orbitalViewCamera.GetComponent<Camera>().orthographic)
        {
            orbitalViewCamera.minSize = EditorGUILayout.FloatField(gcMinSize, orbitalViewCamera.minSize);
            float maxSizeTmp = EditorGUILayout.FloatField(gcMaxSize, orbitalViewCamera.maxSize);

            if (maxSizeTmp < orbitalViewCamera.minSize)
                maxSizeTmp = orbitalViewCamera.minSize;
            
            orbitalViewCamera.maxSize = maxSizeTmp;
        }
        else
        {
            float nearDistanceTmp = EditorGUILayout.FloatField(gcNearDistance, orbitalViewCamera.nearDistance);
            float farDistanceTmp = EditorGUILayout.FloatField(gcFarDistance, orbitalViewCamera.farDistance);

            if (nearDistanceTmp < 0)
                nearDistanceTmp = 0;
            
            if (farDistanceTmp < nearDistanceTmp)
                farDistanceTmp = nearDistanceTmp;
            
            orbitalViewCamera.nearDistance = nearDistanceTmp;
            orbitalViewCamera.farDistance = farDistanceTmp;
        }

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(orbitalViewCamera);
        }
    }
}
