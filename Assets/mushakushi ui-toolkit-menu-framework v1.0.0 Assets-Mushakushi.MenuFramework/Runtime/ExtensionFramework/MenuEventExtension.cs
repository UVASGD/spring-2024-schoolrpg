using System;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Mushakushi.MenuFramework.Runtime.ExtensionFramework
{
    /// <inheritdoc cref="MenuExtension{T}"/>
    public abstract class MenuEventExtension<T>: MenuExtension<T> where T: VisualElement
    {
        /// <summary>
        /// The <see cref="Action"/> to perform on each <see cref="T"/> selected by the <see cref="MenuExtension{T}.Query"/>
        /// when this menu containing this extension is attached to a panel. 
        /// </summary>
        /// <param name="visualElement">The <see cref="T"/> being attached to the panel.</param>
        /// <param name="playerInput"><inheritdoc cref="MenuExtension{T}.Initialize"/></param>
        /// <returns><see cref="Action"/> How to unsubscribe to the event(s).</returns>
        /// <seealso cref="Initialize"/>
        protected abstract Action OnAttach(T visualElement, PlayerInput playerInput);

        /// <summary>
        /// Calls <see cref="OnAttach"/> on each <see cref="T"/> in <see cref="MenuExtension{T}.Query"/>,
        /// while registering the returned <see cref="Action"/> as a <see cref="DetachFromPanelEvent"/> callback. 
        /// </summary>
        /// <inheritdoc cref="MenuExtension{T}.Initialize"/>
        /// <seealso cref="OnAttach"/>
        public sealed override void Initialize(VisualElement container, PlayerInput playerInput)
        {
            var query = Query.EvaluateQuery<T>(container).Descendents<T>().Build();
            foreach (var element in query.ToList())
            {
                var onDetachAction = OnAttach(element, playerInput);
                RegisterDetachCallback(new OnDetachFromPanelArgs<T>(element, onDetachAction));
            }
        }

        /// <summary>
        /// Registers the <see cref="DetachFromPanelEvent"/> event on the <see cref="VisualElement"/>
        /// to call <see cref="OnMenuDetachFromPanel"/>.
        /// </summary>
        /// <param name="args">The <see cref="OnDetachFromPanelArgs{T}"/>
        /// containing the <see cref="VisualElement"/>.
        /// </param>
        private static void RegisterDetachCallback(OnDetachFromPanelArgs<T> args)
        {
            args.visualElement
                .RegisterCallback<DetachFromPanelEvent, OnDetachFromPanelArgs<T>>(OnMenuDetachFromPanel, args);
        }

        /// <summary>
        /// Unsubscribes callbacks as defined by the <see cref="OnDetachFromPanelArgs{T}"/>
        /// on <see cref="DetachFromPanelEvent"/>. 
        /// </summary>
        private static void OnMenuDetachFromPanel(DetachFromPanelEvent _, OnDetachFromPanelArgs<T> args)
        {
            args.visualElement
                .UnregisterCallback<DetachFromPanelEvent, OnDetachFromPanelArgs<T>>(OnMenuDetachFromPanel);
            
            args.onDetach.Invoke();
        }
    }
}