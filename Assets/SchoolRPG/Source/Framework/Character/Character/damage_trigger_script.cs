
using UnityEngine;
namespace damageTrigger
{
    public class DamageTrigger : MonoBehaviour
    {
        public int damageAmount = 1; // Amount of damage to deal
        public GameObject targetObject; // Object to take damage

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == targetObject)
            {
                // Check if the object has a Health component
                targetObject.health.health.takeDamage(1); 
            }
        }
    }
}