using UnityEngine;


namespace SchoolRPG.Inventory.Runtime
{
    public class InventoryManager: MonoBehaviour
    {
        [SerializeField]
        private InventoryEventChannel inventoryEventChannel;
        
        [SerializeField] 
        private Inventory inventory;

        private InventoryItem selectedInventoryItem;

        void Start()
        {
            //inventory.inventoryItems.Clear(); //FINISH FROM HERE
        }

        private void OnEnable()
        {
            inventoryEventChannel.OnAddInventoryItem += AddItem;
            inventoryEventChannel.OnSetSelectedInventoryItem += SetSelectedItem;
        }

        private void OnDisable()
        {
            inventoryEventChannel.OnAddInventoryItem -= AddItem;
            inventoryEventChannel.OnSetSelectedInventoryItem -= SetSelectedItem;
        }

        private void AddItem(InventoryItem newItem)
        {
            for (var i = 0; i < inventory.inventoryItems.Count; i++)
            {
                var item = inventory.inventoryItems[i];
                if (item.DisplayName != newItem.DisplayName) continue;
                item.Count += newItem.Count;
                break;
            }
            inventory.inventoryItems.Add(newItem);
            GameObject.Find(newItem.DisplayName).SetActive(false); // pick up object
        }

        private void SetSelectedItem(InventoryItem inventoryItem)
        {
            selectedInventoryItem = inventoryItem;
            //Debug.Log("Selected Inventory Item: " + selectedInventoryItem.DisplayName);
        }

        public InventoryItem GetSelectedInventoryItem()
        {
            return selectedInventoryItem;
        }
    }
}