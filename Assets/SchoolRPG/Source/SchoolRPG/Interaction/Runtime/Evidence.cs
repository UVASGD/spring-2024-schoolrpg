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

        public override void OnInteract()
        {
            base.OnInteract();
            inventoryEventChannel.RaiseOnAddInventoryItem(inventoryItem);
        }
    }
}
