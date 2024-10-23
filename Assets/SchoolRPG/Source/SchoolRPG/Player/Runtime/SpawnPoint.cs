using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private string doorIdentifier; // The ID of the door this spawn point corresponds to

    private void Start()
    {
        Debug.Log(SceneManagerScript.instance);
        Debug.Log(SceneManagerScript.instance.GetLastDoorUsed());
        if (SceneManagerScript.instance.GetLastDoorUsed() != null && SceneManagerScript.instance.GetLastDoorUsed() == doorIdentifier)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = transform.position;
        }
    }
}
