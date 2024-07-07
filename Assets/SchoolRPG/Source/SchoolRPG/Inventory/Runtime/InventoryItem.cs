using UnityEngine;

namespace SchoolRPG.Inventory.Runtime
{
    /// <summary>
    /// An inventory item. 
    /// </summary>
    [System.Serializable]
    public struct InventoryItem
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
        public string Description { get; set; }

        /// <summary>
        /// The icon that represents this item in UI.
        /// </summary>
        [field: SerializeField]
        public Sprite Icon { get; set; }
        
        /// <summary>
        /// The amount of this item. 
        /// </summary>
        [field: SerializeField]
        public int Count { get; set; }
    }
}