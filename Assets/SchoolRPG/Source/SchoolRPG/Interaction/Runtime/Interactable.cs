using UnityEngine;

namespace SchoolRPG.Interaction.Runtime
{
    public abstract class Interactable : MonoBehaviour
    {
        /// <summary>
        /// Callback when this is interacted with. 
        /// </summary>
        public abstract void OnInteract();
        
    }
}
