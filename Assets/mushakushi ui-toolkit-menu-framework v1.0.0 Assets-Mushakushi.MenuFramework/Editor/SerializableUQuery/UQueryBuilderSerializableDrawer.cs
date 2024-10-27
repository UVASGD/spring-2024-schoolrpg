using Mushakushi.MenuFramework.Runtime.SerializableUQuery;
using UnityEditor;
using UnityEngine;

namespace Mushakushi.MenuFramework.Editor.SerializableUQuery
{
    [CustomPropertyDrawer(typeof(UQueryBuilderSerializable))]
    public class UQueryBuilderSerializableDrawer : PropertyDrawer
    {
        private const string NameOptionsPropertyName = "nameOptions";
        private const string ClassOptionsPropertyName = "classOptions";
        private const string SelectorsPropertyName = nameof(UQueryBuilderSerializable.selectors);
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);
            if (!property.isExpanded) return;
            
            EditorGUI.indentLevel++;
            var selectorsProperty = property.FindPropertyRelative(SelectorsPropertyName); 
            
            EditorGUI.BeginChangeCheck();
            var nameOptionsProperty = property.FindPropertyRelative(NameOptionsPropertyName);
            EditorGUILayout.PropertyField(nameOptionsProperty);
            var wasNameOptionsPropertyModified = EditorGUI.EndChangeCheck();
            
            EditorGUI.BeginChangeCheck();
            var classOptionsProperty = property.FindPropertyRelative(ClassOptionsPropertyName);
            EditorGUILayout.PropertyField(classOptionsProperty);
            var wasClassOptionsPropertyModified = EditorGUI.EndChangeCheck();
            
            EditorGUILayout.PropertyField(selectorsProperty);

            var hasModifiedProperties = property.serializedObject.hasModifiedProperties;
            if (wasNameOptionsPropertyModified || hasModifiedProperties)
            {
                ApplyOptions(selectorsProperty, nameof(Selector.nameOptions), nameOptionsProperty);
            }

            if (wasClassOptionsPropertyModified || hasModifiedProperties)
            {
                ApplyOptions(selectorsProperty, nameof(Selector.classOptions), classOptionsProperty);
            }
            
            EditorGUI.indentLevel--;
        }

        /// <summary>
        /// Clears the array property named <paramref name="optionsPropertyName"/>,
        /// relative to <paramref name="selectorsProperty"/> and copies the elements
        /// in <paramref name="sourceArray"/> into it.
        /// </summary>
        private static void ApplyOptions(SerializedProperty selectorsProperty, string optionsPropertyName, 
            SerializedProperty sourceArray)
        {
            for (var i = 0; i < selectorsProperty.arraySize; i++)
            {
                var selectorOptionsProperty = selectorsProperty
                    .GetArrayElementAtIndex(i)
                    .FindPropertyRelative(optionsPropertyName);
                
                selectorOptionsProperty.ClearArray();
                for (var j = 0; j < sourceArray.arraySize; j++)
                {
                    selectorOptionsProperty.InsertArrayElementAtIndex(j);
                    selectorOptionsProperty.GetArrayElementAtIndex(j).stringValue = 
                        sourceArray.GetArrayElementAtIndex(j).stringValue;
                }
            }
        }
    }
}