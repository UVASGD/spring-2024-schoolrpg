using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Mushakushi.MenuFramework.Runtime.Extensions
{
    /// <summary>
    /// Attach this to the scene to able to quit the application with a certain key. 
    /// </summary>
    public class ApplicationQuitHelper: MonoBehaviour
    {
        /// <summary>
        /// The <see cref="Event.KeyboardEvent"/> key  that will close the application. 
        /// </summary>
        [SerializeField] 
        private string keyName = "escape"; 

        /// <summary>
        /// Quits the a key with name <see cref="keyName"/> is clicked.
        /// </summary>
        private void OnGUI()
        {
            // works even with the old input system disabled
            if (Event.current.Equals(Event.KeyboardEvent(keyName))) ApplicationQuitButtonExtension.QuitApplication();
        }
    }
}