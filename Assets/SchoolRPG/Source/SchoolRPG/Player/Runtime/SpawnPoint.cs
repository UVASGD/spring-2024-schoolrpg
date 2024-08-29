using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private string doorIdentifier; // The ID of the door this spawn point corresponds to

    private void Start()
    {
        if (SceneManagerScript.instance.GetLastDoorUsed() == doorIdentifier)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = transform.position;
        }
    }
}
