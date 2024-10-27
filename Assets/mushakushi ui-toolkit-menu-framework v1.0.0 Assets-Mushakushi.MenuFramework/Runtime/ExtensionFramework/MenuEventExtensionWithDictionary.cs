using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine.UIElements;

namespace Mushakushi.MenuFramework.Runtime.ExtensionFramework
{
    [Serializable]
    public abstract class MenuEventExtensionWithDictionary<TVisualElement, TKey, TValue>: MenuEventExtension<TVisualElement>
        where TVisualElement: VisualElement
    {
        /// <summary>
        /// Serializable dictionary with key <see cref="TKey"/> and value <see cref="TValue"/>. 
        /// </summary>
        protected abstract ElementDictionary Dictionary { get; set; }
        
        [Serializable] public sealed class ElementDictionary: SerializableDictionaryBase<TKey, TValue>{}
    }
}