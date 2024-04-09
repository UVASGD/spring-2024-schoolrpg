
using UnityEngine;
 
namespace damageTrigger
{
    public class DamageTrigger : MonoBehaviour
    {
        public int damageAmount = 1; // Amount of damage to deal
        public GameObject targetObject; // Object to take damage

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == targetObject && other.gameObject.health)
            {
                // Check if the object has a Health component
                targetObject.health.takeDamage(1); 
            }
        }
    }
}