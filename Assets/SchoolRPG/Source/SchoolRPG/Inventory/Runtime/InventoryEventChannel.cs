using System;
using System.Collections.Generic;
using UnityEngine;

namespace SchoolRPG.Inventory.Runtime
{
    /// <summary>
    /// Channel over which to communicate interaction events. 
    /// </summary>
    [CreateAssetMenu(fileName = nameof(InventoryEventChannel), menuName = "ScriptableObjects/Inventory/Inventory Event Channel", order = 0)]
    public class InventoryEventChannel : ScriptableObject
    {
        /// <summary>
        /// Callback when the current inventory item is added.
        /// </summary>
        public Action<InventoryItem> OnAddInventoryItem;

        /// <summary>
        /// Callback on the selected item changed.
        /// </summary>
        public Action<InventoryItem> OnSetSelectedInventoryItem; 

        /// <summary>
        /// Raises the <see cref="OnAddInventoryItem"/> event. 
        /// </summary>
        /// <param name="inventoryItem">The new inventory item.</param>
        public void RaiseOnAddInventoryItem(InventoryItem inventoryItem) => OnAddInventoryItem?.Invoke(inventoryItem);

        /// <summary>
        /// Raises the <see cref="OnSetSelectedInventoryItem"/> event. 
        /// </summary>
        /// <param name="inventoryItem"></param>
        public void RaiseOnSetSelectedInventoryItem(InventoryItem inventoryItem)
        {
            Debug.Log(inventoryItem);
            OnSetSelectedInventoryItem?.Invoke(inventoryItem);
        }
    }
}