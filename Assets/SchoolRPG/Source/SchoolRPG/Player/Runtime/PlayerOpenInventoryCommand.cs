using Mushakushi.MenuFramework.Runtime.ExtensionFramework;
using SchoolRPG.CommandPattern.Runtime;

namespace SchoolRPG.Player.Runtime
{
    public readonly struct PlayerOpenInventoryCommand: ICommand
    {
        private readonly Menu inventoryMenu;
        private readonly MenuEventChannel menuEventChannel;
        private readonly bool isOpen; 

        public PlayerOpenInventoryCommand(Menu inventoryMenu, MenuEventChannel menuEventChannel, bool isOpen)
        {
            this.inventoryMenu = inventoryMenu;
            this.menuEventChannel = menuEventChannel;
            this.isOpen = isOpen;
        }

        public void Execute()
        {
            if (isOpen)
            {
                menuEventChannel.RaiseOnOpenRequested(inventoryMenu);
            }
            else
            {
                menuEventChannel.RaiseOnCloseRequested();
            }
        }
    }
}