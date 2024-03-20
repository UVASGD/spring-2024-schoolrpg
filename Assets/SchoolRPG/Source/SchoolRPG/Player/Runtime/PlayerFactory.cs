using UnityEngine;
using UnityEngine.InputSystem;

namespace SchoolRPG.Player.Runtime
{
    public class PlayerFactory: MonoBehaviour
    {
    
        /// <summary>
        /// The <see cref="PlayerData"/>. 
        /// </summary>
        [field: SerializeField, Tooltip("The player data.")]
        private PlayerData PlayerData { get; set; }


        private void Start()
        {
        }

        private void SpawnPlayer()
        {
            CreateInstance(PlayerData.PlayerPrefab, PlayerData.SpawnPosition); 
        }
    
        private static PlayerInput CreateInstance(GameObject playerPrefab, Vector2 position)
        {
            var instance =  PlayerInput.Instantiate(playerPrefab);
            instance.transform.position = position; 
            return instance; 
        }
    }
}