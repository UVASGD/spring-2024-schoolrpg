using UnityEngine;

namespace SchoolRPG.Inventory.Runtime
{
    /// <summary>
    /// An inventory item. 
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = nameof(InventoryItem), menuName = "ScriptableObjects/Inventory/Inventory Item")]
    public class InventoryItem : ScriptableObject
    {   
        /// <summary>
        /// The name that represents this item. Also used as an id. 
        /// </summary>
        [field: SerializeField]
        public string DisplayName { get; set; }

        /// <summary>
        /// The description for this item in UI.
        /// </summary>
        [field: SerializeField]
        public string DisplayDescription { get; set; }

        /// <summary>
        /// The icon that represents this item in UI.
        /// </summary>
        [field: SerializeField]
        public Sprite Icon { get; set; }

        /// <summary>
        /// The icon that represents this item in UI.
        /// </summary>
        [field: SerializeField]
        public Sprite FullIcon { get; set; }

        /// <summary>
        /// Whether the player has picked up this item or not. 
        /// </summary>
        [field: SerializeField]
        public bool IsPickedUp { get; set; }
    }
}