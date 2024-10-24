using SchoolRPG.Character.Runtime;
using SchoolRPG.Input.Runtime;
using SchoolRPG.Interaction.Runtime;
using SchoolRPG.Inventory.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SchoolRPG.Player.Runtime
{
    /// <summary>
    /// Controls interaction with a single <see cref="Interactable"/> at a time.
    /// </summary>
    public class PlayerInteractionController: MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private Interactable currentInteractable;

        [SerializeField]
        private InputEventChannel inputEventChannel;

        [SerializeField]
        private SceneEventChannel sceneEventChannel;

        [SerializeField]
        private InventoryEventChannel inventoryEventChannel;

        private bool isInventoryOpened = false;
        private InventoryItem dummyItem;
        private CharacterMovementComponent movementComponent;
        
        /// <summary>
        /// The single <see cref="Interactable"/> that is able to interacted, null otherwise.
        /// </summary>
        public Interactable CurrentInteractable => currentInteractable;
        

        private void Start()
        {
            currentInteractable = null;
            dummyItem = ScriptableObject.CreateInstance<InventoryItem>();
            dummyItem.Id = -1;
            movementComponent = GetComponent<CharacterMovementComponent>();
            movementComponent.Activate();
        }

        private void OnEnable()
        {
            inputEventChannel.OnInventory += setIsInventoryOpened;
        }

        private void OnDisable()
        {
            inputEventChannel.OnInventory -= setIsInventoryOpened;
        }
        private void setIsInventoryOpened()
        {
            isInventoryOpened = !isInventoryOpened;
            if (!isInventoryOpened)
            {
                inventoryEventChannel.RaiseOnSetSelectedInventoryItem(dummyItem);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision != null && collision.gameObject.name.Equals("enemy")) { // die and reset level, don't save
                movementComponent.Deactivate();
                sceneEventChannel.RaiseOnPlayerDeathReload(SceneManager.GetActiveScene().name);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // assumes you'll only enter one interactable at a time.
            Debug.Log("Entered other is: " + other);
            if (!other.gameObject.TryGetComponent(out currentInteractable)) return;
            Debug.Log("Attempted grabbed interactable: " + currentInteractable);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("Exited other is: " + other);
            if (!other.gameObject.TryGetComponent<Interactable>(out _)) return;
            currentInteractable = null;
        }

        /// <summary>
        /// If <see cref="CurrentInteractable"/> is not null, interacts with it. 
        /// </summary>
        /// <returns>True if the interaction occured, false otherwise.</returns>
        public bool TryInteract()
        {
            Debug.Log("Attempted interaction with: " + currentInteractable);
            if (!currentInteractable) return false;
            currentInteractable.OnInteract();
            return true;
        }     
    }
}