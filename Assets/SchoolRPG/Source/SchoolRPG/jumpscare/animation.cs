using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;



public class animation : MonoBehaviour
{
    Animator anim = new Animator();
    private GameObject square = GameObject.Find("square");
    private popup_launcher popup_launcher;
    private GameObject gameObject;
    private Collision collision;
    


    
    void Start()
    {
        
        anim = gameObject.GetComponent<Animator>();
    
        anim.SetBool("isRunning", false);
        
        popup_launcher = square.GetComponent<popup_launcher>();
    }

    void ani()
    {
        anim.SetBool("isRunning", true);

    }

    void endAni()
    {
        
        anim.SetBool("isRunning", false);
        popup_launcher popup_launcher = square.GetComponent<popup_launcher>();
        popup_launcher.HidePopUp();


    }

    void Update()
    {
        popup_launcher popup_launcher = square.GetComponent<popup_launcher>();
        
        
        anim = gameObject.GetComponent<Animator>();
        
        AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        float NTime = animStateInfo.normalizedTime;



        bool animationFinished = false;
        if(NTime == 1F) {
            animationFinished = true;
        }

        if(animationFinished == true) {
            endAni();
        }

    }

    private void OnCollisionEnter()
    {
        ani(); 
        popup_launcher popup_launcher = square.GetComponent<popup_launcher>();
        popup_launcher.ShowPopUp();
        
    }
}
 
