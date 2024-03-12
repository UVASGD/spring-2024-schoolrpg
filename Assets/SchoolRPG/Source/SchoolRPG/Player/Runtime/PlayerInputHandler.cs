using SchoolRPG.Input.Runtime;
using SchoolRPG.Source.SchoolRPG.Character.Runtime;
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

        private void OnEnable()
        {
            InputEventChannel.OnMove += HandleInputMove;
            InputEventChannel.OnInteract += HandleInputInteract;
            InputEventChannel.OnActivateInput += HandleActivateInput;
            InputEventChannel.OnActivateInput += HandleDeactivateInput; 
        }
    
        private void OnDisable()
        {
            InputEventChannel.OnMove -= HandleInputMove;
            InputEventChannel.OnInteract -= HandleInputInteract; 
            InputEventChannel.OnActivateInput -= HandleActivateInput;
            InputEventChannel.OnActivateInput -= HandleDeactivateInput; 
        }

        private void HandleInputMove(Vector2 value)
        {
            CharacterMovementComponent.Move(value);
        }

        private void HandleInputInteract()
        {
            // TODO - interaction component here
        }

        private void HandleActivateInput()
        {
            CharacterMovementComponent.Activate();
        }

        private void HandleDeactivateInput()
        {
            CharacterMovementComponent.Deactivate();
        }
    }
}