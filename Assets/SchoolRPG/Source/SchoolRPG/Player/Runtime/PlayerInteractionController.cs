using SchoolRPG.Interaction.Runtime;
using UnityEngine;

namespace SchoolRPG.Player.Runtime
{
    /// <summary>
    /// Controls interaction with a single <see cref="Interactable"/> at a time.
    /// </summary>
    public class PlayerInteractionController: MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private Interactable currentInteractable;
        
        /// <summary>
        /// The single <see cref="Interactable"/> that is able to interacted, null otherwise.
        /// </summary>
        public Interactable CurrentInteractable => currentInteractable;
        

        private void Start()
        {
            currentInteractable = null;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // assumes you'll only enter one interactable at a time.
            if (!other.gameObject.TryGetComponent(out currentInteractable)) return;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent<Interactable>(out _)) return;
            currentInteractable = null;
        }

        /// <summary>
        /// If <see cref="CurrentInteractable"/> is not null, interacts with it. 
        /// </summary>
        /// <returns>True if the interaction occured, false otherwise.</returns>
        public bool TryInteract()
        {
            if (!currentInteractable) return false;
            currentInteractable.OnInteract();
            return true;
        }     
    }
}