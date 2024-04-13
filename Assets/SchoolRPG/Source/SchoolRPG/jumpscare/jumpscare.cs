using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace jumpscare
{
    public class jumpscare : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }
        /*
         * When area object interacts with player
         * Then, trigger the animation attached to the
         * area. since none of that exists tbh I cant test
         * it.
         */

        private void OnTriggerEnter(Collider other)
        {
            //place holder names for real event.
            if (other.gameObject.name == "Player")
            {
                if (gameObject != null)
                {
                    gameObject.animation.animation.ani();
                    other.animation.animation.ani();
                }
            }
        }

        private void OnEventEnd()
        {
            gameObject.animation.animation.endAni();
            other.animation.animation.endAni(); 
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
} 