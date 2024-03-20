using UnityEngine;

namespace SchoolRPG.Player.Runtime
{
    /// <summary>
    /// Stores the prefab and the control configuration of the players.
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player/Data Containers/Player Data")]
    public class PlayerData: ScriptableObject
    {
        /// <summary>
        /// The player Prefab. 
        /// </summary>
        [field: SerializeField, Tooltip("The player Prefab.")]
        public GameObject PlayerPrefab { get; set; }
        
        /// <summary>
        /// The position to spawn the player in. 
        /// </summary>
        [field: SerializeField, Tooltip("The position to spawn the player in.")]
        public Vector2 SpawnPosition { get; set; }
    }
}