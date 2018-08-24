// CAUTION: Don't modify this script.
// This script makes custom inspector of MascotMakerMulti.
using UnityEngine;
using UnityEditor;

namespace DesktopMascotMaker
{
    [CustomEditor(typeof(MascotMakerMulti))]
    public class MascotMakerMultiEditor : Editor
    {
        MascotMakerMulti mascotMakerMulti;

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
            mascotMakerMulti = (MascotMakerMulti)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            mascotMakerMulti.PlayOnAwake = EditorGUILayout.Toggle(gcPlayOnAwake, mascotMakerMulti.PlayOnAwake);
            mascotMakerMulti.TopMost = EditorGUILayout.Toggle(gcTopMost, mascotMakerMulti.TopMost);
            mascotMakerMulti.DragMove = EditorGUILayout.Toggle(gcDragMove, mascotMakerMulti.DragMove);

            mascotFormSize = EditorGUILayout.Vector2Field(gcMascotFormSize, mascotMakerMulti.MascotFormSize);
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
            mascotMakerMulti.MascotFormSize = mascotFormSize;

            mascotMakerMulti.ShowMascotFormOutline = EditorGUILayout.Toggle(gcShowMascotFormOutline, mascotMakerMulti.ShowMascotFormOutline);
            mascotMakerMulti.AntiAliasing = (MascotMakerMulti.AntiAliasingType)EditorGUILayout.EnumPopup(gcAntiAliasing, mascotMakerMulti.AntiAliasing);
            
            if (mascotMakerMulti.ChromaKeyCompositing = EditorGUILayout.Toggle(gcChromaKeyCompositing, mascotMakerMulti.ChromaKeyCompositing))
            {
                mascotMakerMulti.ChromaKeyColor = EditorGUILayout.ColorField(gcChromaKeyColor, mascotMakerMulti.ChromaKeyColor);
                mascotMakerMulti.ChromaKeyRange = EditorGUILayout.Slider(gcChromaKeyRange, mascotMakerMulti.ChromaKeyRange, 0.002f, 0.5f);
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(mascotMakerMulti);
            }
        }
    }
}
