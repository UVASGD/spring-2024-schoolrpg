using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Mushakushi.MenuFramework.Runtime.SerializableUQuery
{
    /// <summary>
    /// Creates and serializes a pseudo-<see cref="UQueryBuilder{T}"/>. 
    /// </summary>
    [Serializable]
    public struct UQueryBuilderSerializable
    {
        [SerializeField]
        private List<string> nameOptions;

        public List<string> NameOptions
        {
            get => nameOptions;
            set => SetNameOptions(value);
        }
        
        [SerializeField]
        private List<string> classOptions;

        public List<string> ClassOptions
        {
            get => classOptions;
            set => SetClassOptions(value);
        }

        public Selector[] selectors;

        /// <summary>
        /// Sets <see cref="nameOptions"/>, and applies the change
        /// to all <see cref="selectors"/>.
        /// </summary>
        public void SetNameOptions(List<string> value)
        {
            nameOptions = value;
            foreach (var selector in selectors)
            {
                selector.nameOptions = value;
            }
        }

        /// <summary>
        /// Sets <see cref="classOptions"/>, and applies the change
        /// to all <see cref="selectors"/>.
        /// </summary>
        public void SetClassOptions(List<string> value)
        {
            classOptions = value;
            foreach (var selector in selectors)
            {
                selector.classOptions = value; 
            }
        }

        /// <summary>
        /// Converts a <see cref="UQueryBuilderSerializable"/> into a <see cref="UQueryBuilder{T}"/>
        /// based on the current selectors. 
        /// </summary>
        /// <param name="visualElement">
        /// The <see cref="VisualElement"/> to build the query on. 
        /// </param>
        /// <typeparam name="T">
        /// The type of <see cref="VisualElement"/> to query for.
        /// </typeparam>
        /// <returns>
        /// A <see cref="UQueryBuilder{T}"/> representation of the current object. 
        /// </returns>
        /// <remarks>
        /// The resulting query is built in sequential order based on the <see cref="selectors"/>.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// A <see cref="SelectorType"/> is invalid. 
        /// </exception>
        public UQueryBuilder<T> EvaluateQuery<T>(VisualElement visualElement) where T : VisualElement
        {
            var uQueryBuilder = new UQueryBuilder<T>(visualElement);
            
            foreach (var selector in selectors)
            {
                switch (selector.type)
                {
                    case SelectorType.Name:
                        foreach (var name in selector.names)
                            uQueryBuilder.Name(name);
                        break;
                    case SelectorType.Class:
                        foreach (var className in selector.classes)
                            uQueryBuilder.Name(className);
                        break;
                    case SelectorType.PseudoState:
                        switch (selector.state)
                        {
                            case PseudoSelector.Active:
                                uQueryBuilder.Active();
                                break;
                            case PseudoSelector.Checked:
                                uQueryBuilder.Checked();
                                break;
                            case PseudoSelector.Enabled:
                                uQueryBuilder.Enabled();
                                break;
                            case PseudoSelector.Focused:
                                uQueryBuilder.Focused();
                                break;
                            case PseudoSelector.Hovered:
                                uQueryBuilder.Hovered();
                                break;
                            case PseudoSelector.Visible:
                                uQueryBuilder.Visible();
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case SelectorType.NegativePseudoState:
                        switch (selector.state)
                        {
                            case PseudoSelector.Active:
                                uQueryBuilder.NotActive();
                                break;
                            case PseudoSelector.Checked:
                                uQueryBuilder.NotChecked();
                                break;
                            case PseudoSelector.Enabled:
                                uQueryBuilder.NotEnabled();
                                break;
                            case PseudoSelector.Focused:
                                uQueryBuilder.NotFocused();
                                break;
                            case PseudoSelector.Hovered:
                                uQueryBuilder.NotHovered();
                                break;
                            case PseudoSelector.Visible:
                                uQueryBuilder.NotVisible();
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case SelectorType.Wildcard:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return uQueryBuilder; 
        }
    }
}