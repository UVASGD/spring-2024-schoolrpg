using JetBrains.Annotations;
using Mushakushi.MenuFramework.Runtime.SerializableUQuery;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Mushakushi.MenuFramework.Runtime.ExtensionFramework
{
    /// <summary>
    /// Controls a menu. 
    /// </summary>
    public class MenuController : MonoBehaviour
    {
        /// <summary>
        /// The <see cref="UIDocument"/> that displays all the menus.
        /// </summary>
        [Header("UI"), SerializeField] 
        private UIDocument rootDocument;

        /// <summary>
        /// The name of the <see cref="VisualElement"/> that contains all menus. 
        /// </summary>
        /// <remarks>
        /// This element is cleared and populated with <see cref="VisualTreeAsset"/> menus during runtime.
        /// </remarks>
        [field: SerializeField, NameClassSelector(nameof(rootDocument))] 
        protected string RootContainerName { get; set; }
        
        /// <summary>
        /// The class name of the initial focused element. 
        /// </summary>
        [field: SerializeField, NameClassSelector(nameof(rootDocument))] 
        protected string InitialFocusedElementClassName { get; set; }
        
        /// <summary>
        /// The current container holding all the menus. 
        /// </summary>
        private VisualElement RootContainer { get; set; }
        
        /// <summary>
        /// The <see cref="menuEventChannel"/>. 
        /// </summary>
        [Header("Channels"), SerializeField]
        private MenuEventChannel menuEventChannel;
        
        /// <summary>
        /// The input action asset being used for this project.
        /// </summary>
        [field: SerializeField]
        public PlayerInput CurrentPlayerInput { get; [UsedImplicitly] private set; }
        
        /// <summary>
        /// The <see cref="IMenuExtension"/>(s) applied to the <see cref="rootDocument"/> root <see cref="VisualElement"/>. 
        /// </summary>
        [field: SerializeReference, SubclassSelector, Header("Extensions")] 
        private IMenuExtension[] GlobalExtensions { get; [UsedImplicitly] set; }

        protected virtual void OnEnable()
        {
            menuEventChannel.OnOpenRequested += Open;
            menuEventChannel.OnCloseRequested += Close;
            menuEventChannel.OnPopulateRequested += Populate;
        }
        
        protected virtual void OnDisable()
        {
            menuEventChannel.OnOpenRequested -= Open;
            menuEventChannel.OnCloseRequested -= Close;
            menuEventChannel.OnPopulateRequested -= Populate;
        }

        private void Awake()
        {
            RootContainer = string.IsNullOrEmpty(RootContainerName)
                ? rootDocument.rootVisualElement
                : rootDocument.rootVisualElement.Q<VisualElement>(RootContainerName);
            
            Close();
        }

        /// <summary>
        /// Shows a menu. 
        /// </summary>
        /// <param name="menu">The <see cref="Menu"/></param>
        private void Open(Menu menu)
        {
            Populate(menu);
            rootDocument.rootVisualElement.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            menuEventChannel.RaiseOnOpenCompleted();
        }

        /// <summary>
        /// Hides the menu. 
        /// </summary>
        private void Close()
        {
            rootDocument.rootVisualElement.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None); 
        }

        /// <summary>
        /// Populates a <see cref="Menu"/>, and the focuses an element within it called 
        /// </summary>
        /// <param name="menu">The <see cref="Menu"/> to populate.</param>
        /// <param name="isAdditive">
        /// If true, the <see cref="RootContainer"/> is cleared before populating the new menu.
        /// </param>
        private void Populate(Menu menu, bool isAdditive = false)
        {
            if (menu == null || RootContainer == null) return;
            
            if (!isAdditive) RootContainer.Clear();
            menu.Asset.CloneTree(RootContainer);

            var initialFocusedElement = RootContainer.Q<VisualElement>(InitialFocusedElementClassName);
            if (initialFocusedElement == null || InitialFocusedElementClassName.Length == 0)
            {
                RootContainer.Focus();
            }
            else
            {
                initialFocusedElement.Focus();
            }

            foreach (var extension in GlobalExtensions) extension?.Initialize(rootDocument.rootVisualElement, CurrentPlayerInput);
            foreach (var extension in menu.Extensions) extension?.Initialize(RootContainer, CurrentPlayerInput);
            menu.MenuConnections?.Initialize(RootContainer, CurrentPlayerInput);
        }

        /// <summary>
        /// Focuses the the Visual Element on the <see cref="RootContainer"/> that matches a <paramref name="query"/>.
        /// </summary>
        public bool FocusElement<T>(UQueryState<T> query) where T : VisualElement
        {
            if (RootContainer == null) return false;
            var element = query.RebuildOn(RootContainer).First();
            if (element == null) return false;
            element.Focus();
            return true;
        }
    }
}