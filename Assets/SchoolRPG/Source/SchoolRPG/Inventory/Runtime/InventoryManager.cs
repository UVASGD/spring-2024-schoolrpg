using UnityEngine;


namespace SchoolRPG.Inventory.Runtime
{
    public class InventoryManager: MonoBehaviour
    {
        [SerializeField]
        private InventoryEventChannel inventoryEventChannel;
        
        [SerializeField] 
        private PlayerInventory inventory;

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
            Debug.Log(newItem.DisplayName);
            GameObject _item = GameObject.Find(newItem.DisplayName);
            Collider2D collider2D = _item.GetComponent<Collider2D>();
            SpriteRenderer spriteRenderer = _item.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
                spriteRenderer.enabled = false;
            if (collider2D != null)
                collider2D.enabled = false;
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