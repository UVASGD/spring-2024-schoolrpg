using System;
using System.Collections.Generic;
using UnityEngine;

namespace SchoolRPG.Dialogue.Runtime
{
    /// <summary>
    /// Event channel for dialogue events. 
    /// </summary>
    [CreateAssetMenu(fileName = nameof(SceneEventChannel), menuName = "ScriptableObjects/SceneManagement/Scene Event Channel", order = 0)]
    public class SceneEventChannel : ScriptableObject
    {
        /// <summary>
        /// Callback when the door open is triggered. 
        /// </summary>
        public event Action OnOpenDoorRequested;

        /// <inheritdoc cref= "OnOpenDoorRequested"/>
        public void RaiseOnOpenDoorRequested() => OnOpenDoorRequested?.Invoke();
    }
}
