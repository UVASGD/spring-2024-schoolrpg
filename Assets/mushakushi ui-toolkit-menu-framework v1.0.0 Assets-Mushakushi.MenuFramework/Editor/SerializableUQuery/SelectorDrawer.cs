using Mushakushi.MenuFramework.Runtime.SerializableUQuery;
using UnityEditor;
using UnityEngine;

namespace Mushakushi.MenuFramework.Editor.SerializableUQuery
{
    [CustomPropertyDrawer(typeof(Selector))]
    public class SelectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var typeProperty = property.FindPropertyRelative(nameof(Selector.type));
            var typePropertyRect = GetNextRect(position, typeProperty); 
            EditorGUI.PropertyField(typePropertyRect, typeProperty);
            position.y += typePropertyRect.height;
            
            var nextProperty = GetNextProperty(property, typeProperty);
            if (nextProperty == null) return;

            EditorGUI.indentLevel++; 
            EditorGUI.PropertyField(GetNextRect(position, nextProperty), nextProperty);
            EditorGUI.indentLevel--; 
        }

        private static Rect GetNextRect(Rect position, SerializedProperty property)
        {
            return new Rect(position.x, position.y, position.width,
                EditorGUI.GetPropertyHeight(property) + EditorGUIUtility.standardVerticalSpacing);
        }

        private static SerializedProperty GetNextProperty(SerializedProperty property)
        {
            var typeProperty = property.FindPropertyRelative(nameof(Selector.type));
            return GetNextProperty(property, typeProperty);
        }

        private static SerializedProperty GetNextProperty(SerializedProperty property, 
            SerializedProperty typeProperty)
        {
            switch ((SelectorType)typeProperty.enumValueIndex)
            {
                case SelectorType.Name:
                    return property.FindPropertyRelative(nameof(Selector.names));
                case SelectorType.PseudoState:
                case SelectorType.NegativePseudoState:
                    return property.FindPropertyRelative(nameof(Selector.state));
                case SelectorType.Class:
                    return property.FindPropertyRelative(nameof(Selector.classes));
                case SelectorType.Wildcard:
                default:
                    return null;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var nextProperty = GetNextProperty(property);
            return base.GetPropertyHeight(property, label)
                   + (nextProperty == null ? 0 : EditorGUI.GetPropertyHeight(nextProperty))
                   + EditorGUIUtility.standardVerticalSpacing;
        }
    }
}