using System;
using UnityEngine.UIElements;

namespace Mushakushi.MenuFramework.Runtime.ExtensionFramework
{
    /// <summary>
    /// Describes a <see cref="VisualElement"/> that is detaching from a panel. 
    /// Callback args for <see cref="MenuEventExtension{T}.OnMenuDetachFromPanel"/>. 
    /// </summary>
    /// <seealso cref="MenuEventExtension{T}.OnMenuDetachFromPanel"/>
    internal struct OnDetachFromPanelArgs<T> where T : VisualElement
    {
        public readonly T visualElement;
            
        public readonly Action onDetach;
        
        /// <param name="visualElement">
        /// The <see cref="VisualElement"/> that is detaching from the panel. 
        /// </param>
        /// <param name="onDetach">
        /// The callback to perform. 
        /// </param>
        public OnDetachFromPanelArgs(T visualElement, Action onDetach)
        {
            this.visualElement = visualElement;
            this.onDetach = onDetach;
        }
    }
}