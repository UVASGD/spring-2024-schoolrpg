using System;
using UnityEngine;

namespace Mushakushi.MenuFramework.Runtime.ExtensionFramework
{
    [CreateAssetMenu(fileName = nameof(MenuEventChannel), menuName = "ScriptableObjects/UI/Menu Event Channel", order = 0)]
    public class MenuEventChannel : ScriptableObject
    {
        /// <summary>
        /// Callback on <see cref="Menu"/> open request. 
        /// </summary>
        public event Action<Menu> OnOpenRequested;

        /// <summary>
        /// Callback on <see cref="Menu"/> open. 
        /// </summary>
        public event Action OnOpenCompleted;

        /// <summary>
        /// Callback on <see cref="Menu"/> close.
        /// </summary>
        public event Action OnCloseRequested;

        /// <summary>
        /// Callback on <see cref="Menu"/> populate on the <see cref="MenuController"/>.
        /// </summary>
        public event Action<Menu, bool> OnPopulateRequested;

        /// <summary>
        /// Raises the <see cref="OnOpenRequested"/> event. 
        /// </summary>
        /// <param name="menu">The <see cref="Menu"/> to open.</param>
        public void RaiseOnOpenRequested(Menu menu)
        { 
            OnOpenRequested?.Invoke(menu);
        }

        /// <summary>
        /// Raises the <see cref="OnOpenCompleted"/> event. 
        /// </summary>
        public void RaiseOnOpenCompleted()
        {
            OnOpenCompleted?.Invoke();
        }

        /// <summary>
        /// Raises the <see cref="OnCloseRequested"/> event.
        /// </summary>
        public void RaiseOnCloseRequested()
        {
            OnCloseRequested?.Invoke();
        }

        /// <summary>
        /// Raises the <see cref="OnPopulateRequested"/> event.
        /// </summary>
        /// <param name="menu">The menu to open.</param>
        /// <param name="isAdditive">
        /// Whether or not this menu should be appended to any currently open menu(s).
        /// </param>
        public void RaiseOnPopulateRequested(Menu menu, bool isAdditive = false)
        {
            OnPopulateRequested?.Invoke(menu, isAdditive);
        }
    }
}