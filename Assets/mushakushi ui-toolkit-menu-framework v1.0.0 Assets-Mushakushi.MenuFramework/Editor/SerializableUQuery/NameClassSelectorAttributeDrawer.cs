using System;
using System.Collections.Generic;
using System.Linq;
using Mushakushi.MenuFramework.Runtime.SerializableUQuery;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Mushakushi.MenuFramework.Editor.SerializableUQuery
{
    [CustomPropertyDrawer(typeof(NameClassSelectorAttribute))]
    public class NameClassSelectorAttributeDrawer : PropertyDrawer
    {
        /// <summary>
        /// Option to display in pop up when there is no selectors to show,
        /// wrapped in an array.
        /// </summary>
        /// <remarks>This can happen if there is not visual element selected.</remarks>
        private readonly string[] emptyPopoutText = { "No Selectors Available!" };
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var type = fieldInfo.FieldType;
            if (type != typeof(string) && type != typeof(string[]) && type != typeof(IList<string>))
            {
                EditorGUI.PropertyField(position, property, label);
                return; 
            }
            
            var uiSelectorAttribute = (NameClassSelectorAttribute)attribute;
            
            var selectorsContainerObject = ReflectionUtility
                .FindPropertyRelativeAsObject(property, uiSelectorAttribute.selectorsContainerName);
            
            if (selectorsContainerObject == null)
            {
                EditorGUI.PropertyField(position, property, label);
                return; 
            }
            
            var options = GetSelectors(selectorsContainerObject, uiSelectorAttribute.mode, new string[]{});
            if (options == null || options.Length == 0)
            {
                if (uiSelectorAttribute.showEmptyPopup)
                {
                    EditorGUI.Popup(position, property.displayName, 0, emptyPopoutText);
                }
                else
                {
                    EditorGUI.PropertyField(position, property, label);
                }
                return; 
            }

            EditorGUI.BeginChangeCheck();
            
            var guiEvent = Event.current;
            if (guiEvent.type == EventType.MouseDown && guiEvent.button == 1 && position.Contains(guiEvent.mousePosition))
            {
                var contextMenu = new GenericMenu();
                contextMenu.AddItem(new GUIContent("Unset"), 
                    string.IsNullOrEmpty(property.stringValue), 
                    () =>
                    {
                        property.stringValue = string.Empty;
                        property.serializedObject.ApplyModifiedProperties(); 
                    });
                contextMenu.ShowAsContext(); 
            }
            
            var selectedIndex = EditorGUI
                .Popup(position, property.displayName, Array.IndexOf(options, property.stringValue), options);
            property.stringValue = options.ElementAtOrDefault(selectedIndex); 
            
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RegisterCompleteObjectUndo(property.serializedObject.targetObject, 
                    $"Modified Selector '{property.displayName}' in {property.serializedObject.targetObject.name}");
            }
        }
        
        /// <summary>
        /// The selectors in <see cref="selectorsContainer"/>,
        /// null otherwise. 
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="mode"/> is invalid.
        /// </exception>
        /// <remarks>
        /// See documentation for <see cref="NameClassSelectorAttribute"/> for valid types of containers. 
        /// </remarks>
        private static string[] GetSelectors(object selectorsContainer, SelectorMode mode, 
            IEnumerable<string> baseOptions)
        {
            VisualElement visualElement; 
            switch (selectorsContainer)
            {
                case VisualTreeAsset visualTreeAsset:
                    visualElement = visualTreeAsset.CloneTree();
                    break;
                case UIDocument uiDocument:
                    visualElement = uiDocument.rootVisualElement;
                    break;
                case string[] selectors:
                    return selectors;
                case IList<string> selectors:
                    return selectors.ToArray();
                default:
                    return null;
            }
            return mode switch
            {
                SelectorMode.Name => baseOptions.Concat(GetAllNames(visualElement)).ToArray(),
                
                SelectorMode.Class => baseOptions.Concat(GetAllStyleClasses(visualElement)).ToArray(),
                
                SelectorMode.All => baseOptions.Concat(GetAllNames(visualElement))
                    .Concat(GetAllStyleClasses(visualElement)).ToArray(),
                
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };
        }
        
        
        /// <returns>
        /// All the names on a visual element including its children.
        /// </returns>
        private static IEnumerable<string> GetAllNames(VisualElement element)
        {
            return element.Query().ToList().Select(x => x.name);
        }

        /// <returns>
        /// All style classes on a visual element including its children.
        /// </returns>
        private static IEnumerable<string> GetAllStyleClasses(VisualElement element)
        {
            return element.Query().ToList()
                .SelectMany(x => x.GetClasses())
                .Distinct();
        }

        // private static IEnumerable<string> GetAllNamesAndStyleClasses(VisualElement element)
        // {
        //     return GetAllNames(element).Select(x => $"Names/{x}")
        //         .Concat(GetAllStyleClasses(element).Select(x => $"Classes/{x}"));
        // }
    }
}