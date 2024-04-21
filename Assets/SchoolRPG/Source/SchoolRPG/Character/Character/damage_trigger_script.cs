
using UnityEngine;
 
namespace damageTrigger
{
    public class DamageTrigger : MonoBehaviour
    {
        public int damageAmount = 1; // Amount of damage to deal
        public GameObject player;

        public static bool dealingDamage = false;
        private bool hitting = false;


        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.Equals(player) && !dealingDamage)
            {
                // player take damage
                dealingDamage = true;
                player.GetComponent<health_controller>().takeDamage(1, player.GetComponent<SpriteRenderer>());
            }
        }
    }
}