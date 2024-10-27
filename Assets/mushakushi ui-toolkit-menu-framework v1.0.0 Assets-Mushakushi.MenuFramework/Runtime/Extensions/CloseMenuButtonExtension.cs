using System;
using Mushakushi.MenuFramework.Runtime.ExtensionFramework;
using Mushakushi.MenuFramework.Runtime.SerializableUQuery;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Mushakushi.MenuFramework.Runtime.Extensions
{
    /// <summary>
    /// Requests to close the menu on click.
    /// </summary>
    [Serializable]
    public class CloseMenuButtonExtension: MenuEventExtension<Button>
    {
        [SerializeField] private MenuEventChannel menuEventChannel;
        [field: SerializeField] public override UQueryBuilderSerializable Query { get; protected set; }

        protected override Action OnAttach(Button visualElement, PlayerInput playerInput)
        {
            visualElement.clicked += menuEventChannel.RaiseOnCloseRequested;
            return () => visualElement.clicked -= menuEventChannel.RaiseOnCloseRequested;
        }
    }
}