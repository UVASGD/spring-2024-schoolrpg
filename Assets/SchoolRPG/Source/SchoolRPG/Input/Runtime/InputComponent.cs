using UnityEngine;
using UnityEngine.InputSystem;

namespace SchoolRPG.Input.Runtime
{
    public class InputComponent : MonoBehaviour
    {
        /// <summary>
        /// The <see cref="PlayerInput"/>.
        /// </summary>
        [SerializeField, Tooltip("The Player Input Component.")] 
        private PlayerInput playerInput;

        /// <summary>
        /// The <see cref="InputEventChannel"/> to send input events to. 
        /// </summary>
        [SerializeField, Tooltip("The InputEventChannel to send input events to.")]
        private InputEventChannel inputEventChannel; 
    
        public void OnMove(InputAction.CallbackContext callbackContext)
        {
            // if (value.canceled) return; 
            inputEventChannel.RaiseOnMove(callbackContext.ReadValue<Vector2>());
        }

        public void OnInteract(InputAction.CallbackContext callbackContext)
        {
            if (!callbackContext.started || !playerInput.inputIsActive) return;
            inputEventChannel.RaiseOnInteract();
        }

        public void OnInventory(InputAction.CallbackContext callbackContext)
        {
            if (!callbackContext.started || !playerInput.inputIsActive) return;
            inputEventChannel.RaiseOnInventory();
        }

        public void DeactivateInput()
        {
            playerInput.DeactivateInput();
        }

        public void ActivateInput()
        {
            playerInput.ActivateInput();
        }
    }
}
