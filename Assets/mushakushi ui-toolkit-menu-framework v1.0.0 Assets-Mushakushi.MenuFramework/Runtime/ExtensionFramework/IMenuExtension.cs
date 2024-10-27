using Mushakushi.MenuFramework.Runtime.SerializableUQuery;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Mushakushi.MenuFramework.Runtime.ExtensionFramework
{
    public interface IMenuExtension
    {
        /// <summary>
        /// The query to retrieve the <see cref="VisualElement"/>(s) being operated on. 
        /// </summary>
        public UQueryBuilderSerializable Query { get; }

        /// <summary>
        /// Initializes the extension. 
        /// </summary>
        /// <param name="container">
        /// The <see cref="VisualElement"/> container of the <see cref="VisualElement"/>(s) being operated on,
        /// built <see cref="MenuExtension{T}.Query"/> on.
        /// </param>
        /// <param name="playerInput">
        /// The <see cref="PlayerInput"/> that should be considered (e.g. is in use) when using the menu. 
        /// </param>
        public void Initialize(VisualElement container, PlayerInput playerInput);
    }
}