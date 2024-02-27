using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static death.Death;

namespace health
{
    public class health : MonoBehaviour
    {
        // Start is called before the first frame update
        public int maxHealth = 3;
        public int currentHealth;
        public bool alive;
        void Start()
        {
            currentHealth = maxHealth;
            alive = true;
        }

        public void takeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                alive = false;
            }
        }
        // Update is called once per frame
        void Update()
        {

        }

    }
}