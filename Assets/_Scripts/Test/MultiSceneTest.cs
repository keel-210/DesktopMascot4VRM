using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSceneTest : MonoBehaviour
{
    void Start()
    {
        GameObject obj = FindObjectOfType<Quote>().gameObject;
        Debug.Log(obj?.name);
    }
}