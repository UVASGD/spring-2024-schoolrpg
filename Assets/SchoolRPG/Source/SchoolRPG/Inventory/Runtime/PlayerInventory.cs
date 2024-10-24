using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace SchoolRPG.Inventory.Runtime
{
    /// <summary>
    /// Represents a player inventory. 
    /// </summary>
    [CreateAssetMenu(fileName  = nameof(Inventory), menuName = "ScriptableObjects/Inventory/Inventory")]
    public class PlayerInventory: ScriptableObject
    {
        /// <summary>
        /// The list of inventory items. 
        /// </summary>
        
        public List<InventoryItem> inventoryItems;

        public bool contains(InventoryItem item)
        {
            return inventoryItems.Contains(item);
        }
    }
}
