using System;
using UnityEngine;

namespace SchoolRPG.Input.Runtime
{
    /// <summary>
    /// Sends and receives event relating to input. 
    /// </summary>
    [CreateAssetMenu(fileName = "Input Event Channel", menuName = "ScriptableObjects/Input/Channels/Input Event Channel")]
    public class InputEventChannel: ScriptableObject
    {
        /// <summary>
        /// Callback on movement axis value changed. 
        /// </summary>
        public event Action<Vector2> OnMove;
    
        /// <summary>
        /// Callback on interact button pressed.  
        /// </summary>
        public event Action OnInteract;

        /// <summary>
        /// Callback on input activated. 
        /// </summary>
        public event Action OnActivateInput;

        /// <summary>
        /// Callback on input deactivated. 
        /// </summary>
        public event Action OnDeactivateInput;

        /// <summary>
        /// Callback on inventory button pressed.
        /// </summary>
        public event Action OnInventory; 

        /// <summary>
        /// Raises the <see cref="OnMove"/> event. 
        /// </summary>
        /// <param name="value">The value of the movement.</param>
        public void RaiseOnMove(Vector2 value) => OnMove?.Invoke(value);

        /// <summary>
        /// Raises the <see cref="OnInteract"/> event. 
        /// </summary>
        public void RaiseOnInteract() => OnInteract?.Invoke();

        /// <summary>
        /// Raises the <see cref="OnInventory"/> event.
        /// </summary>
        public void RaiseOnInventory()
        {
            Debug.Log("Inventory activated");
            OnInventory?.Invoke();
        }

        /// <summary>
        /// Raises the <see cref="OnActivateInput"/> event. 
        /// </summary>
        public void RaiseOnActivateInput() => OnActivateInput?.Invoke();

        /// <summary>
        /// Raises the <see cref="OnDeactivateInput"/> event. 
        /// </summary>
        public void RaiseOnDeactivateInput() => OnDeactivateInput?.Invoke(); 
    }
}
