// CAUTION: Don't modify this script.
// This script makes custom inspector of MascotMaker.
using UnityEngine;
using UnityEditor;

namespace DesktopMascotMaker
{
    [CustomEditor(typeof(MascotMaker))]
    public class MascotMakerEditor : Editor
    {
        MascotMaker mascotMaker;

        GUIContent gcPlayOnAwake = new GUIContent("Play On Awake", "If set to true, the mascot will automatically show on awake.");
        GUIContent gcTopMost = new GUIContent("Top Most", "If set to true, the mascot will be displayed as a topmost form.");
        GUIContent gcDragMove = new GUIContent("Drag Move", "If set to true, the mascot can be moved by left mouse button dragging.");
        GUIContent gcMascotFormSize = new GUIContent("Mascot Form Size", "Specify Mascot Form size here. You can change the Mascot Form's size at runtime.");
        GUIContent gcShowMascotFormOutline = new GUIContent("Show Mascot Form Outline", "This checkbox is for debug. If set to true, Mascot Form's outline will be visible and you can check its size.");
        GUIContent gcAntiAliasing = new GUIContent("Anti Aliasing", "Specify anti-aliasing level here.");
        GUIContent gcChromaKeyCompositing = new GUIContent("Chroma Key Compositing", "If set to true, Chroma Key Compositing mode will be ON.");
        GUIContent gcChromaKeyColor = new GUIContent("Chroma Key Color", "Chroma key background color. This color will be transparent.");
        GUIContent gcChromaKeyRange = new GUIContent("Chroma Key Range", "Chroma key compositing intensity. [0.002 - 0.5]");

        Vector2 mascotFormSize;

        void OnEnable()
        {
            mascotMaker = (MascotMaker)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            mascotMaker.PlayOnAwake = EditorGUILayout.Toggle(gcPlayOnAwake, mascotMaker.PlayOnAwake);
            mascotMaker.TopMost = EditorGUILayout.Toggle(gcTopMost, mascotMaker.TopMost);
            mascotMaker.DragMove = EditorGUILayout.Toggle(gcDragMove, mascotMaker.DragMove);

            mascotFormSize = EditorGUILayout.Vector2Field(gcMascotFormSize, mascotMaker.MascotFormSize);
            // maximum size limitation
            if (mascotFormSize.x > 2000)
                mascotFormSize.x = 2000;
            if (mascotFormSize.y > 2000)
                mascotFormSize.y = 2000;
            // minimum size mlimitation
            if (mascotFormSize.x < 1)
                mascotFormSize = new Vector2(1, (int)mascotFormSize.y);
            if (mascotFormSize.y < 1)
                mascotFormSize = new Vector2((int)mascotFormSize.x, 1);
            mascotMaker.MascotFormSize = mascotFormSize;

            mascotMaker.ShowMascotFormOutline = EditorGUILayout.Toggle(gcShowMascotFormOutline, mascotMaker.ShowMascotFormOutline);
            mascotMaker.AntiAliasing = (MascotMaker.AntiAliasingType)EditorGUILayout.EnumPopup(gcAntiAliasing, mascotMaker.AntiAliasing);

            if (mascotMaker.ChromaKeyCompositing = EditorGUILayout.Toggle(gcChromaKeyCompositing, mascotMaker.ChromaKeyCompositing))
            {
                mascotMaker.ChromaKeyColor = EditorGUILayout.ColorField(gcChromaKeyColor, mascotMaker.ChromaKeyColor);
                mascotMaker.ChromaKeyRange = EditorGUILayout.Slider(gcChromaKeyRange, mascotMaker.ChromaKeyRange, 0.002f, 0.5f);
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(mascotMaker);
            }
        }
    }
}