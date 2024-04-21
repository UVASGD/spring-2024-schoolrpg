using SchoolRPG.Character.Runtime;
using SchoolRPG.Input.Runtime;
using UnityEngine;

namespace SchoolRPG.Player.Runtime
{
    /// <summary>
    /// Handles 
    /// </summary>
    public class PlayerInputHandler: MonoBehaviour
    {
        /// <summary>
        /// The <see cref="InputEventChannel"/> to receive input events from.
        /// </summary>
        [field: SerializeField, Tooltip("The " + nameof(InputEventChannel) + " to receive input events from")]
        public InputEventChannel InputEventChannel { get; private set; }
    
        /// <summary>
        /// The <see cref="CharacterMovementComponent"/> that handles movement. 
        /// </summary>
        [field: SerializeField, Tooltip("The " + nameof(CharacterMovementComponent) + " that handles movement")]
        public CharacterMovementComponent CharacterMovementComponent { get; private set; }
        
        /// <summary>
        /// The <see cref="PlayerInteractionController"/> that controls interaction.
        /// </summary> 
        [field: SerializeField]
        public PlayerInteractionController PlayerInteractionController { get; private set; }
        
        [field: SerializeField]
        public PlayerNextDialogueCommand PlayerNextDialogueCommand { get; private set; }

        private void OnEnable()
        {
            InputEventChannel.OnMove += HandleOnMove;
            InputEventChannel.OnInteract += HandleOnInteract;
            InputEventChannel.OnActivateInput += HandleOnActivateInput;
            InputEventChannel.OnActivateInput += HandeOnDeactivateInput; 
        }
    
        private void OnDisable()
        {
            InputEventChannel.OnMove -= HandleOnMove;
            InputEventChannel.OnInteract -= HandleOnInteract; 
            InputEventChannel.OnActivateInput -= HandleOnActivateInput;
            InputEventChannel.OnActivateInput -= HandeOnDeactivateInput; 
        }

        private void HandleOnMove(Vector2 value)
        {
            CharacterMovementComponent.Move(value);
        }

        private void HandleOnInteract()
        {
            PlayerInteractionController.TryInteract();
        }

        private void HandleOnActivateInput()
        {
            CharacterMovementComponent.Activate();
        }

        private void HandeOnDeactivateInput()
        {
            CharacterMovementComponent.Deactivate();
        }
    }
}