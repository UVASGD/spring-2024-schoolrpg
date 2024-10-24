using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event channel for dialogue events. 
/// </summary>
[CreateAssetMenu(fileName = nameof(SceneEventChannel), menuName = "ScriptableObjects/SceneManagement/Scene Event Channel", order = 0)]
public class SceneEventChannel : ScriptableObject
{
    /// <summary>
    /// Callback when the door open is triggered. 
    /// </summary>
    public event Action<string> OnOpenDoorRequested;

    /// <summary>
    /// Callback when the scene needs to be reloaded when the player dies.  
    /// </summary>
    public event Action<string> OnPlayerDeathReload;

    /// <inheritdoc cref= "OnOpenDoorRequested"/>
    public void RaiseOnOpenDoorRequested(string doorName) => OnOpenDoorRequested?.Invoke(doorName);

    /// <inheritdoc cref= "OnPlayerDeathReload"/>
    public void RaiseOnPlayerDeathReload(string scene) => OnPlayerDeathReload?.Invoke(scene);
}

