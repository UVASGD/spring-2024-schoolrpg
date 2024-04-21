using System;
using Mushakushi.MenuFramework.Runtime.ExtensionFramework;
using Mushakushi.MenuFramework.Runtime.SerializableUQuery;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace SchoolRPG.Inventory.Runtime
{
    [Serializable]
    public class SelectedItemNameExtension: MenuEventExtension<Label>
    {
        [field: SerializeField]
        public override UQueryBuilderSerializable Query { get; protected set; }
        
        [SerializeField] 
        private InventoryEventChannel inventoryEventChannel; 
        
        protected override Action OnAttach(Label label, PlayerInput playerInput)
        {
            inventoryEventChannel.OnSetSelectedInventoryItem += SetName;
            return () => inventoryEventChannel.OnSetSelectedInventoryItem -= SetName;

            void SetName(InventoryItem inventoryItem)
            {
                Debug.Log(inventoryItem.DisplayName);
                label.text = inventoryItem.DisplayName;
            }
        }
    }
}