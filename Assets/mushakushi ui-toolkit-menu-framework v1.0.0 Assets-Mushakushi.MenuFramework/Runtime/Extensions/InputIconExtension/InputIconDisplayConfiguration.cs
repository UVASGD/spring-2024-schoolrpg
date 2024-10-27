using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.InputSystem;

// ReSharper disable once CheckNamespace
namespace Mushakushi.MenuFramework.Runtime.Extensions
{
    /// <summary>
    /// Manages all <see cref="InputIconMap"/>. 
    /// </summary>
    [CreateAssetMenu(fileName = nameof(InputIconDisplayConfiguration), menuName = "ScriptableObjects/Input/Input Icon Display Configuration", order = 0)]
    public class InputIconDisplayConfiguration : ScriptableObject
    {
        /// <summary>
        /// Maps each device's raw path to their <see cref="InputIconMap"/>. 
        /// </summary>
        [field: Header("Device Sets"), Tooltip("Maps each device raw path to their InputIconMap."), SerializeField]
        public DeviceInputIconSets DeviceInputIconMap { get; private set; }
        [Serializable]
        public sealed class DeviceInputIconSets: SerializableDictionaryBase<string, InputIconMap>{}

        /// <summary>
        /// Gets a suitable device name of the first device on a <see cref="PlayerInput"/> component
        /// with respect to the <see cref="DeviceInputIconMap"/>. 
        /// </summary>
        /// <returns><see cref="string"/> The device name.</returns>
        public string GetDeviceName(PlayerInput playerInput)
        {
            return DeviceInputIconMap[playerInput.devices[0].ToString()]?.DisplayName;
        }

        /// <summary>
        /// Get the input icon of a raw control path with respect to the <see cref="DeviceInputIconMap"/>.
        /// </summary>
        /// <returns><see cref="Texture"/> The input icon.</returns>
        public Texture2D GetDeviceBindingIcon(string rawControlPath)
        {
            var rawControlPaths = rawControlPath.Split('/', 2);
            return DeviceInputIconMap[rawControlPaths[0]]?.Icons[rawControlPaths[1]];
        }

        /// <summary>
        /// Return the raw binding path of an <see cref="InputAction"/> by a control scheme, otherwise null
        /// if the binding path could not be found. 
        /// </summary>
        /// <param name="action">The <see cref="InputAction"/> to get a raw binding path for.</param>
        /// <param name="controlScheme">The control scheme.</param>
        /// <param name="options">Optional set of formatting flags.</param>
        /// <returns><see cref="string"/> The raw binding path.</returns>
        public static string GetActionBindingPath(InputAction action, string controlScheme,
            InputBinding.DisplayStringOptions options = 0)
        {
            if (action == null) return null;
            var bindingIndex = action.GetBindingIndex(InputBinding.MaskByGroup(controlScheme));
            if (bindingIndex < 0) return null;
            action.GetBindingDisplayString(bindingIndex, out var deviceLayoutName, out var controlPath, options);
            return $"<{deviceLayoutName}>/{controlPath}";
        }
    }
}