using Mushakushi.MenuFramework.Runtime.SerializableUQuery;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Mushakushi.MenuFramework.Runtime.ExtensionFramework {     
    
    /// <summary>
    /// An extension to <see cref="MenuController"/>. 
    /// </summary>
    /// <typeparam name="T">The <see cref="VisualElement"/>s that are queried for.</typeparam>
    public abstract class MenuExtension<T>: IMenuExtension       
        where T: VisualElement
    {
        /// <inheritdoc />
        public abstract UQueryBuilderSerializable Query { get; protected set; }
        
        /// <inheritdoc />
        public abstract void Initialize(VisualElement container, PlayerInput playerInput);
    } 
}