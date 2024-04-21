using Mushakushi.MenuFramework.Runtime.ExtensionFramework;
using SchoolRPG.CommandPattern.Runtime;
using SchoolRPG.Input.Runtime;
using UnityEngine;

namespace SchoolRPG.Player.Runtime
{
    public class PlayerOpenInventoryCommandHandler: MonoBehaviour
    {
        [SerializeField] 
        private MenuEventChannel menuEventChannel;

        [SerializeField] 
        private Menu inventoryMenu;

        [SerializeField] 
        private InputEventChannel inputEventChannel;

        private bool isClosed; 

        private void OnEnable()
        {
            inputEventChannel.OnInventory += HandleOnInteract; 
        }

        private void OnDisable()
        {
            inputEventChannel.OnInventory -= HandleOnInteract;
        }

        private void HandleOnInteract()
        {
            var playerOpenInventoryCommand = new PlayerOpenInventoryCommand(inventoryMenu, menuEventChannel, !isClosed);
            CommandInvoker.Execute(playerOpenInventoryCommand);
            isClosed = !isClosed;
        }
    }
}