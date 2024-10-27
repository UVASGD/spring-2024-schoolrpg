using System;
using Mushakushi.MenuFramework.Runtime.ExtensionFramework;
using Mushakushi.MenuFramework.Runtime.SerializableUQuery;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Mushakushi.MenuFramework.Runtime.Extensions
{
    /// <summary>
    /// Connects multiple menus together with one extension. 
    /// </summary>
    [Serializable]
    public class MenuConnectionButtonExtension: MenuEventExtension<Button>
    {
        /// <summary>
        /// Maps a button, by name, to the menu that it wants to open. 
        /// </summary>
        [field: SerializeField]
        private ButtonToMenuDictionary ButtonNameToMenu { get; set; }
        
        [SerializeField] private MenuEventChannel menuEventChannel;
        [field: SerializeField] public override UQueryBuilderSerializable Query { get; protected set; }
        [Serializable] public sealed class ButtonToMenuDictionary: SerializableDictionaryBase<string, Menu>{}
        
        protected override Action OnAttach(Button button, PlayerInput playerInput)
        {
            button.clicked += PopulateMenu;
            return () => button.clicked -= PopulateMenu;

            void PopulateMenu()
            {
                if (ButtonNameToMenu.TryGetValue(button.name, out var menu))
                    menuEventChannel.RaiseOnPopulateRequested(menu);
            }
        }
        
        [Serializable]
        public struct MenuConnection
        {
            [NameClassSelector(nameof(UQueryBuilderSerializable.selectors))]
            public string name;
        }

    }
}