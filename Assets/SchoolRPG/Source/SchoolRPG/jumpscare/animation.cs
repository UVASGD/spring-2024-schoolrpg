using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using pop_up_launcher;

namespace animation {
    public class animation : MonoBehaviour
    {
        Animator anim = new Animator();
        private GameObject square = GameObject.Find("square");
        private popup_launcher popup_launcher = square.GetComponent<popup_launcher>();
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
            popup_launcher.HidePopUp();

        }

        void Update()
        {
            
            public  Animator animator;
            AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            private  NTime  = animStateInfo.normalizedTime;
            
         
 
            bool animationFinished;
            if(NTime == 1F) {
                animationFinished = true; 
            }
            if(animationFinished == True) {
                endAni(); 
                
            }

    
    } 
} 