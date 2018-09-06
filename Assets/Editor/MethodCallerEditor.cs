using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (MethodCaller))]
public class MethodClassEditor : Editor
{

    MethodCaller m_Target = null;

    string[] m_Methods = new string[] { };

    /// <summary>
    /// 収集したメソッドをクリア
    /// </summary>
    void ClearMethods ()
    {
        m_Methods = new string[] { "None" };
    }

    /// <summary>
    /// 登録されたオブジェクトのPublicメソッドを収集
    /// </summary>
    void CollectMethods ()
    {
        if (m_Target == null || m_Target.objMono == null)
        {
            ClearMethods ();
            return;
        }
        ArrayList result = new ArrayList ();
        result.Add ("None");

        string[] methodsName = m_Target.objMono.GetType ().GetMethods (BindingFlags.Instance | BindingFlags.Public)
            .Where (x => x.DeclaringType == m_Target.objMono.GetType ())
            .Where (x => x.GetParameters ().Length == 0)
            .Select (x => x.Name)
            .ToArray ();
        result.AddRange (methodsName);
        m_Methods = (string[])result.ToArray (typeof (string));
    }

    void OnEnable ()
    {
        m_Target = target as MethodCaller;
        CollectMethods ();
    }

    public override void OnInspectorGUI ()
    {
        base.OnInspectorGUI ();

        CollectMethods ();

        if (m_Target == null)
        {
            return;
        }

        if (m_Methods.Length == 0)
        {
            return;
        }

        int index = ArrayUtility.IndexOf (m_Methods, m_Target.CallbackName);
        if (index != -1)
        {
            EditorGUILayout.LabelField ("Trigger methods");
            m_Target.CallbackName = m_Methods[EditorGUILayout.Popup (index, m_Methods)];
        }

    }
}