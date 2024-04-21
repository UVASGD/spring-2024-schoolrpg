using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using pop_up_launcher;
using UnityEngine;

namespace jumpscare
{
    public class jumpscare : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        private BoxCollider BoxCollider; 

        private GameObject player = GameObject.Find("player");
        GameObject square = GameObject.Find("square");
        popup_launcher popup_launcher = square.GetComponent<popup_launcher>();

        
        private Collision collision;
        private GameObject other = GameObject.Find(Collision.collider.name); 

        /*
         * When area object interacts with player
         * Then, trigger the animation attached to the
         * area. since none of that exists tbh I cant test
         * it.
         */

        private void OnCollisionEnter()
        {
            
            //place holder names for real event.
            if (other.gameObject.name == "Player")
            {
                if (gameObject != null)
                {
                    
                    ani.ani();
                    otherAnimation.ani(); 
                    popup_launcher.ShowPopUp();
                    
                }
            }
        }



        // Update is called once per frame
        void Update()
        {

        }
    }
} 