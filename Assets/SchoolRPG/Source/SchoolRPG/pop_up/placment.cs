using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class KeepAboveParent2D : MonoBehaviour {
    private Transform parentTransform;
    
    void Start()
    {

    parentTransform = transform.parent;
    if (parentTransform == null)
        {
            Debug.LogError("Parent transform not found!");
            return;
        }
    }

    void Update()
    {
    if (parentTransform == null)
    {
        Debug.LogError("Parent transform not found!");
        
    }


    transform.position = new Vector3(parentTransform.position.x, parentTransform.position.y + 1f, transform.position.z);
    }
}