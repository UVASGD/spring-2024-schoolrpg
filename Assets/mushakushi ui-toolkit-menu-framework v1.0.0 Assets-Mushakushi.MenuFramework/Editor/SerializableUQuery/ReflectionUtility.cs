// modified from:
// https://github.com/lordofduct/spacepuppy-unity-framework/blob/master/SpacepuppyBaseEditor/EditorHelper.cs

using System;
using System.Collections;
using System.Reflection;
using UnityEditor;

namespace Mushakushi.MenuFramework.Editor.SerializableUQuery
{
    public static class ReflectionUtility
    {
        /// <summary>
        /// Retrieves the target object of a serialized property, navigating through its hierarchy.
        /// </summary>
        /// <param name="property">The serialized property whose target object is to be retrieved.</param>
        /// <param name="currentTargetObject">The initial object from which to start the search.</param>
        /// <returns>The target object of the specified serialized property if found; otherwise, null.</returns>
        public static object GetParentObjectOfProperty(SerializedProperty property, object currentTargetObject)
        {
            if (property == null) return null; 
            var path = property.propertyPath.Replace(".Array.data[", "[");
            var elements = path.Split('.');
            
            foreach (var element in elements)
            {
                object nextTargetObject; 
                
                if (element.Contains("["))
                {
                    var arrayPrefixStartIndex = element.IndexOf("[", StringComparison.Ordinal);
                    var elementName = element[..arrayPrefixStartIndex];
                    var index = Convert.ToInt32(element[(arrayPrefixStartIndex + 1)..]
                        .Replace("]", ""));
                    nextTargetObject = GetCollectionElementFieldValue(currentTargetObject, elementName, index);
                }
                else
                {
                    nextTargetObject = GetFieldValue(currentTargetObject, element);
                }
                
                var type = nextTargetObject?.GetType();
                if (type == null || IsPrimitiveOrString(type) || IsPrimitiveOrStringCollection(type)) break;
                currentTargetObject = nextTargetObject;
            }
            
            return currentTargetObject;
        }
        
        /// <inheritdoc cref="GetParentObjectOfProperty(SerializedProperty, object)"/>
        public static object GetParentObjectOfProperty(SerializedProperty property)
        {
            return GetParentObjectOfProperty(property, property.serializedObject.targetObject);
        }

        /// <summary>
        /// Returns true if <paramref name="type"/> is a primitive type or <see cref="string"/>.
        /// </summary>
        public static bool IsPrimitiveOrString(Type type)
        {
            return type.IsPrimitive || type.Name == nameof(String); 
        }

        /// <summary>
        /// Returns true if <paramref name="type"/> is an <see cref="IEnumerable"/>
        /// of type <see cref="string"/> or another primitive type.
        /// </summary>
        /// <seealso cref="IsPrimitiveOrString"/>
        public static bool IsPrimitiveOrStringCollection(Type type)
        {
            return type.GetInterface(nameof(IEnumerable)) != null
                && IsPrimitiveOrString(type.GetGenericArguments()[0]); 
        }

        /// <summary>
        /// Get the relative property <paramref name="property"/> with a certain property path. 
        /// </summary>
        /// <param name="property">The property to start looking from.</param>
        /// <param name="relativePropertyPath">The relative property path to <paramref name="property"/>.</param>
        /// <returns>The relative property as an <see cref="object"/>.</returns>
        /// <seealso cref="GetParentObjectOfProperty(SerializedProperty)"/>
        /// <seealso cref="SerializedProperty.FindPropertyRelative"/>
        public static object FindPropertyRelativeAsObject(SerializedProperty property, string relativePropertyPath)
        {
            var targetObject = GetParentObjectOfProperty(property);
            return targetObject?.GetType() 
                .GetField(relativePropertyPath,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                ?.GetValue(targetObject);
        }

        /// <summary>
        /// Retrieves the value of a field or property from the specified object.
        /// </summary>
        /// <param name="source">The object from which to retrieve the value.</param>
        /// <param name="name">The name of the field or property whose value is to be retrieved.</param>
        /// <returns>The value of the specified field or property if found; otherwise, null.</returns>
        /// <remarks>
        /// This method searches for both public and non-public fields and properties,
        /// including those inherited from base classes. It ignores the case when searching for properties.
        /// </remarks>
        public static object GetFieldValue(object source, string name)
        {
            if (source == null) return null;
            
            var type = source.GetType();
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

            while (type != null)
            {
                var fieldInfo = type.GetField(name, flags);
                if (fieldInfo != null)
                {
                    return fieldInfo.GetValue(source);
                }

                var propertyInfo = type.GetProperty(name, flags | BindingFlags.IgnoreCase);
                if (propertyInfo != null)
                {
                    return propertyInfo.GetValue(source, null);
                }

                type = type.BaseType;
            }
            
            return null;
        }
        
        /// <summary>
        /// Retrieves an element at a specified index from a collection property or field within the source object.
        /// </summary>
        /// <param name="source">The object containing the collection.</param>
        /// <param name="name">The name of the collection field or property.</param>
        /// <param name="index">The zero-based index of the element to retrieve from the collection.</param>
        /// <returns>The element at the specified index within the collection if found; otherwise, null.</returns>
        /// <remarks>
        /// The method first retrieves the collection by name using <see cref="GetFieldValue"/>.
        /// It then attempts to return the element at the specified index. This method assumes the collection
        /// implements <see cref="System.Collections.IEnumerable"/>.
        /// </remarks>
        public static object GetCollectionElementFieldValue(object source, string name, int index)
        {
            if (GetFieldValue(source, name) is not IEnumerable enumerable) return null;
            var enumerator = enumerable.GetEnumerator();

            for (var i = 0; i <= index; i++)
            {
                if (!enumerator.MoveNext()) return null;
            }

            var res = enumerator.Current;
            (enumerator as IDisposable)?.Dispose();
            return res;
        }
    }
}