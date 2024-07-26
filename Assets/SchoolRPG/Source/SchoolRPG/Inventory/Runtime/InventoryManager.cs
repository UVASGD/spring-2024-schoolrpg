using UnityEngine;

namespace SchoolRPG.Inventory.Runtime
{
    public class InventoryManager: MonoBehaviour
    {
        [SerializeField]
        private InventoryEventChannel inventoryEventChannel;

        [SerializeField] 
        private Inventory inventory;

        private void OnEnable()
        {
            inventoryEventChannel.OnAddInventoryItem += AddItem;
        }

        private void OnDisable()
        {
            inventoryEventChannel.OnAddInventoryItem -= AddItem;
        }

        private void AddItem(InventoryItem newItem)
        {
            for (var i = 0; i < inventory.inventoryItems.Count; i++)
            {
                var item = inventory.inventoryItems[i];
                if (item.DisplayName != newItem.DisplayName) continue;
                item.Count += newItem.Count;
                return;
            }
            Debug.Log(newItem.DisplayName);
            Debug.Log(newItem.DisplayDescription);
            inventory.inventoryItems.Add(newItem);
            GameObject.Find(newItem.DisplayName).SetActive(false); // pick up object
        }
    }
}