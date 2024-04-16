using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace animation { 
public class animation: MonoBehaviour
{
    Animator anim = new Animator();
    private GameObject gameObject;

    void Start()
    {

        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("isRunning", false);
    }

    void ani()
    {
        anim.SetBool("isRunning", true);
    }

    void endAni()
    {
        anim.SetBool("isRunning", false);
        
    }
} 
    } 
} 