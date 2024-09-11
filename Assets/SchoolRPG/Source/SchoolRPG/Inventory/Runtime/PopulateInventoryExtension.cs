using System;
using System.Collections.Generic;
using System.Linq;
using Mushakushi.MenuFramework.Runtime.ExtensionFramework;
using Mushakushi.MenuFramework.Runtime.SerializableUQuery;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace SchoolRPG.Inventory.Runtime
{
    /// <summary>
    /// Displays the inventory UI. 
    /// </summary>
    [Serializable]
    public class PopulateInventoryExtension : MenuEventExtension<ListView>
    {
        [field: SerializeField]
        public override UQueryBuilderSerializable Query { get; protected set; }
        
        [SerializeField]
        private VisualTreeAsset inventoryItemAsset;

        [SerializeField, NameClassSelector(nameof(inventoryItemAsset), SelectorMode.Name)]
        private string inventoryItemAssetImageName; 
        
        /// <summary>
        /// The class name of the label that displays the count of an item.
        /// </summary>
        [SerializeField, NameClassSelector(nameof(inventoryItemAsset), SelectorMode.Name)]
        private string inventoryItemAssetCountLabelName; 
        
        /// <summary>
        /// The inventory items to display.
        /// </summary>
        [SerializeField]
        private PlayerInventory inventory;

        [SerializeField] 
        private InventoryEventChannel inventoryEventChannel; 
        
        protected override Action OnAttach(ListView listView, PlayerInput playerInput)
        {
            listView.Clear();
            
            listView.selectionType = SelectionType.Single;
            listView.itemsSource = inventory.inventoryItems;
            listView.makeItem = MakeItem;
            listView.bindItem = BindItem;
            
            listView.RefreshItems();

            listView.selectionChanged += OnSelectionChanged; 
            return () =>
            {
                listView.selectionChanged -= OnSelectionChanged;
            }; 
        }

        private VisualElement MakeItem()
        {
            return inventoryItemAsset.Instantiate();
        }

        private void BindItem(VisualElement itemVisualElement, int index)
        {
            var item = inventory.inventoryItems[index];
            itemVisualElement.Q<VisualElement>(inventoryItemAssetImageName).style.backgroundImage = new StyleBackground(item.Icon);
            itemVisualElement.Q<Label>(inventoryItemAssetCountLabelName).text = item.Count <= 1 
                ? string.Empty : item.Count.ToString();
            itemVisualElement.style.width = new StyleLength(100);
            itemVisualElement.style.height = new StyleLength(100);
        }

        private void OnSelectionChanged(IEnumerable<object> enumerable)
        {
            var objects = enumerable as object[] ?? enumerable.ToArray();
            var selectedItem = objects.First() is InventoryItem inventoryItem
                ? inventoryItem 
                : default;
            inventoryEventChannel.RaiseOnSetSelectedInventoryItem(selectedItem);
        }
    }
}
