using Eflatun.SceneReference;
using UnityEngine;

namespace SchoolRPG.SceneManagement.Runtime
{
    /// <summary>
    /// Data container that stores the context of scene
    /// </summary>
    [CreateAssetMenu(fileName = "SceneData", menuName = "ScriptableObjects/Scene/Data Containers/Scene Data", order = 0)]
    public class SceneData : ScriptableObject
    {
        /// <summary>
        /// The scene.
        /// </summary>
        [field: Header("Scene"), Tooltip("The scene."), SerializeField] 
        public SceneReference SceneReference { get; private set; }
        
        /// <summary>
        /// Whether or not a loading screen should be shown when loading this scene. 
        /// </summary>
        [field: Tooltip("Whether or not a loading screen should be shown."), SerializeField]
        public bool ShowLoadingScreen { get; private set; }
    }
}