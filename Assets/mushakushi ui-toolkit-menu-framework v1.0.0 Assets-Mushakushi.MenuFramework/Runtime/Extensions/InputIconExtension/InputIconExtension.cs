using System;
using Mushakushi.MenuFramework.Runtime.ExtensionFramework;
using Mushakushi.MenuFramework.Runtime.SerializableUQuery;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Mushakushi.MenuFramework.Runtime.Extensions
{
    /// <summary>
    /// Sets the background image of a <see cref="VisualElement"/> to match with the input icon defined by its name.
    /// </summary>
    [Serializable]
    public class InputIconExtension: MenuEventExtension<VisualElement>
    {
        [SerializeField] 
        private InputIconDisplayConfiguration inputIconDisplayConfiguration;
        
        [field: SerializeField] public override UQueryBuilderSerializable Query { get; protected set; }

        protected override Action OnAttach(VisualElement visualElement, PlayerInput playerInput)
        {
            SetInputIcon(visualElement, playerInput);
            return null;
        }

        /// <summary>
        /// Sets the input icon to its appropriate icon by name.
        /// It is imperative these icons are named in format: $"{action}-{controlScheme}-icon".
        /// </summary>
        /// <param name="visualElement">The input icon.</param>
        /// <param name="playerInput"></param>
        private void SetInputIcon(VisualElement visualElement, PlayerInput playerInput)
        {
            var controlScheme = GetControlSchemeNameFromVisualElement(visualElement, playerInput.currentControlScheme);
            var inputAction = GetInputActionNameFromVisualElement(visualElement);
            var controlPath = InputIconDisplayConfiguration.GetActionBindingPath(
                playerInput.currentActionMap.FindAction(inputAction), controlScheme);
            visualElement.style.backgroundImage = 
                new StyleBackground(inputIconDisplayConfiguration.GetDeviceBindingIcon(controlPath));
        }

        /// <summary>
        /// From a <see cref="VisualElement"/>, get it's corresponding
        /// <see cref="UnityEngine.InputSystem.InputAction"/> name.
        /// The default implementation assumes it is the second element
        /// in a dash-delimited string (e.g. ...-{inputAction})
        /// </summary>
        protected virtual string GetInputActionNameFromVisualElement(VisualElement visualElement)
        {
            return visualElement.name.Split('-', 3)[1];
        }
        
        /// <summary>
        /// From a <see cref="VisualElement"/>, get it's corresponding
        /// <see cref="UnityEngine.InputSystem.InputControlScheme"/> name.
        /// The default implementation assumes it is the first element
        /// in a dash-delimited string (e.g. {controlScheme}-...),
        /// Additionally if the control scheme name is "current", <paramref name="defaultControlScheme"/>
        /// is used. 
        /// </summary>
        protected virtual string GetControlSchemeNameFromVisualElement(VisualElement visualElement, 
            string defaultControlScheme)
        {
            var controlScheme = visualElement.name.Split('-', 3)[1];
            return controlScheme is "current" ? defaultControlScheme : controlScheme;
        }
    }
}