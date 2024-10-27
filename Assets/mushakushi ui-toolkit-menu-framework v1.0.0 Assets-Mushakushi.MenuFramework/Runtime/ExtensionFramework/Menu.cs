using JetBrains.Annotations;
using Mushakushi.MenuFramework.Runtime.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace Mushakushi.MenuFramework.Runtime.ExtensionFramework
{
    [UsedImplicitly, CreateAssetMenu(fileName = nameof(Menu), menuName = "ScriptableObjects/UI/Menu", order = 0)]
    public class Menu: ScriptableObject
    {
        /// <summary>
        /// The <see cref="VisualTreeAsset"/> of the submenu. 
        /// </summary>
        [field: SerializeField] 
        public VisualTreeAsset Asset { get; private set; }

        /// <summary>
        /// The <see cref="IMenuExtension"/>(s). 
        /// </summary>
        [field: SerializeReference, SubclassSelector] 
        public IMenuExtension[] Extensions { get; [UsedImplicitly] protected set; }

        /// <summary>
        /// The <see cref="Menu"/>(s) that can be navigated to via this.
        /// </summary>
        /// <remarks>
        /// Separate by design from <see cref="Extensions"/> on the basis that most <see cref="Menu"/>s include them.
        /// </remarks>
        [field: SerializeField] 
        public MenuConnectionButtonExtension MenuConnections { get; private set; }
    }
}