using SchoolRPG.Inventory.Runtime;
using UnityEngine;

namespace SchoolRPG.Interaction.Runtime
{
    public class Evidence : TalkScript
    {
        [SerializeField]
        private InventoryItem inventoryItem;

        [SerializeField]
        private InventoryEventChannel inventoryEventChannel;

        public override void Start()
        {
            base.Start();
            // despawn if collected on load
            if (inventoryItem.Collected)
            {
                Collider2D collider2D = GetComponent<Collider2D>();
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                    spriteRenderer.enabled = false;
                if (collider2D != null)
                    collider2D.enabled = false;
            } 
        }

        public override void OnInteract()
        {
            base.OnInteract();
            inventoryEventChannel.RaiseOnAddInventoryItem(inventoryItem);
            inventoryItem.Collected = true;
        }
    }
}
