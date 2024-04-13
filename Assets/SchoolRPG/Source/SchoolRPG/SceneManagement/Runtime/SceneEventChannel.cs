using System;
using UnityEngine;

namespace SchoolRPG.SceneManagement.Runtime
{
    /// <summary>
    /// Event on which GameObjects get a callback with relevant level data on load.
    /// Called on start callback with important contextual information about the current scene. 
    /// </summary>
    [CreateAssetMenu(fileName = "SceneEventChannel", menuName = "ScriptableObjects/Scene/Channels/Scene")]
    public class SceneEventChannel : ScriptableObject
    {
        /// <summary>
        /// Callback on scene load request.
        /// </summary>
        public event Action<SceneData> OnLoadRequested;

        /// <summary>
        /// Callback on loading screen shown. 
        /// </summary>
        public event Action OnLoadingScreenDisplayed; 

        /// <summary>
        /// Callback when all async loading and unloading operations are completed. 
        /// </summary>
        public event Action OnAsyncOperationsCompleted;

        /// <summary>
        /// Callback on scene is ready to be shown to the player. 
        /// </summary>
        /// <remarks>
        /// Although <see cref="OnAsyncOperationsCompleted"/> may have fired, each scene needs
        /// to perform functions, such as spawning before the scene is ready to be shown. 
        /// </remarks>
        public event Action OnSceneReady; 

        /// <summary>
        /// Callback on scene load progress. 
        /// </summary>
        public event Action<float> OnLoadProgress; 

        /// <summary>
        /// Callback on restart request.
        /// </summary>
        public event Action OnRestartRequested;

        /// <summary>
        /// Raises the <see cref="OnLoadRequested"/> event. 
        /// </summary>
        /// <param name="sceneData">The <see cref="SceneData"/> of the loading scene.</param>
        public void RaiseOnLoadRequested(SceneData sceneData)
        {
            OnLoadRequested?.Invoke(sceneData);
        }
        
        /// <summary>
        /// Raises the <see cref="OnAsyncOperationsCompleted"/> event. 
        /// </summary>
        public void RaiseOnAsyncOperationsCompleted()
        {
            OnAsyncOperationsCompleted?.Invoke();
        }

        /// <summary>
        /// Raises the <see cref="OnLoadingScreenDisplayed"/> event. 
        /// </summary>
        public void RaiseOnLoadingScreenShown()
        {
            OnLoadingScreenDisplayed?.Invoke();
        }

        /// <summary>
        /// Raises the <see cref="OnSceneReady"/> event. 
        /// </summary>
        public void RaiseOnSceneReady()
        {
            OnSceneReady?.Invoke();
        }
        
        /// <summary>
        /// Raises the <see cref="OnLoadProgress"/> event. 
        /// </summary>
        /// <param name="progress">The normalized progress of the scene loading.</param>
        public void RaiseOnLoadProgress(float progress)
        {
            OnLoadProgress?.Invoke(progress);
        }

        /// <summary>
        /// Raises the <see cref="OnRestartRequested"/> event. 
        /// </summary>
        public void RaiseOnRestartRequested()
        {
            OnRestartRequested?.Invoke();
        }
    }
}