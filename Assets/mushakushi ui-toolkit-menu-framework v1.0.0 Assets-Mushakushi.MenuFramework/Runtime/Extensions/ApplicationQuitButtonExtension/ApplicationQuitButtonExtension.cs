using System;
using Mushakushi.MenuFramework.Runtime.ExtensionFramework;
using Mushakushi.MenuFramework.Runtime.SerializableUQuery;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Mushakushi.MenuFramework.Runtime.Extensions
{
    [Serializable]
    public class ApplicationQuitButtonExtension: MenuEventExtension<Button>
    {
        [field: SerializeField] public override UQueryBuilderSerializable Query { get; protected set; }

        private GameObject applicationQuitHelper;

        protected override Action OnAttach(Button visualElement, PlayerInput playerInput)
        {
            visualElement.clicked += QuitApplication;

            applicationQuitHelper = new GameObject("Keyboard Application Quit Helper", typeof(ApplicationQuitHelper));
            
            return () =>
            {
                visualElement.clicked -= QuitApplication;
                UnityEngine.Object.Destroy(applicationQuitHelper);
                applicationQuitHelper = null;
            };
        }

        /// <summary>
        /// Quits the application in build mode, and stops playing in the Unity Editor.
        /// </summary>
        public static void QuitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}