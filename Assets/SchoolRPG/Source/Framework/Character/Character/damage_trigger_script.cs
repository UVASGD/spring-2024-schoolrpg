using static player.Healthcontrol.Healthcontorloer;
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
                Health health = other.GetComponent<Health>();
                if (health != null)
                {
                    // Deal damage to the target object
                    health.TakeDamage(damageAmount);
                }
            }
        }
    }
}