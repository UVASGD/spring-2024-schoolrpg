using System;
using Mushakushi.MenuFramework.Runtime.ExtensionFramework;
using Mushakushi.MenuFramework.Runtime.SerializableUQuery;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Mushakushi.MenuFramework.Runtime.Extensions
{
    /// <summary>
    /// Invokes arbitrary <see cref="UnityEvent{Button}"/> events on <see cref="Button"/> click. 
    /// </summary>
    [Serializable]
    public class UnityEventButtonExtension : MenuEventExtension<Button>
    {
        [SerializeField]
        private UnityEvent<Button>[] callbacks;
        
        [field: SerializeField]
        public override UQueryBuilderSerializable Query { get; protected set; }

        protected override Action OnAttach(Button visualElement, PlayerInput playerInput)
        {
            visualElement.clicked += Callback;
            return () => visualElement.clicked -= Callback;
            void Callback ()
            {
                foreach (var unityEvent in callbacks) unityEvent.Invoke(visualElement);
            }
        }
    }
}