// CAUTION: Don't modify this script.
// This script makes custom inspector of MascotMaker.
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MainWindowOpacity))]
public class MainWindowOpacityEditor : Editor
{
    MainWindowOpacity mainWindowOpacity;

    GUIContent gcMainWindowOpacity = new GUIContent("Main Window Opacity", "Main window's opacity [0 - 255]. If set to 0, main window will disappear.");

    void OnEnable()
    {
        mainWindowOpacity = (MainWindowOpacity)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.HelpBox("If you want to hide Unity's main window, attach this script and change Main Window Opacity value to 0." +
                        " You can attach this scirpt anywhere in your scene. \n" +
                        " \n If set to 0, main window will be disappeared." +
                        " \n If set to [1-254], main window will be transparented." +
                        " \n If set to 255, nothing happens.\n" +
                        " \n Note : This script only works in build(released) programs." 
                        , MessageType.Info);

        mainWindowOpacity.Opacity = EditorGUILayout.IntSlider(gcMainWindowOpacity, mainWindowOpacity.Opacity, 0, 255);

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(mainWindowOpacity);
        }
    }
}
