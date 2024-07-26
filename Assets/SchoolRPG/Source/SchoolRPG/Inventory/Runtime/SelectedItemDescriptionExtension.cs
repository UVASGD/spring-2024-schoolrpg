using System;
using Mushakushi.MenuFramework.Runtime.ExtensionFramework;
using Mushakushi.MenuFramework.Runtime.SerializableUQuery;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace SchoolRPG.Inventory.Runtime
{
    [Serializable]
    public class SelectedItemDescriptionExtension: MenuEventExtension<Label>
    {
        [field: SerializeField]
        public override UQueryBuilderSerializable Query { get; protected set; }
        
        [SerializeField] 
        private InventoryEventChannel inventoryEventChannel; 
        
        protected override Action OnAttach(Label label, PlayerInput playerInput)
        {
            inventoryEventChannel.OnSetSelectedInventoryItem += SetDescription;
            return () => inventoryEventChannel.OnSetSelectedInventoryItem -= SetDescription;

            void SetDescription(InventoryItem inventoryItem)
            {
                Debug.Log(inventoryItem.DisplayDescription);
                label.text = inventoryItem.DisplayDescription;
            }
        }
    }
}