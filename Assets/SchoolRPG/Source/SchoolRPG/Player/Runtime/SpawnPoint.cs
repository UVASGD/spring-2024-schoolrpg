using UnityEngine;
using System;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private string doorIdentifier; // The ID of the door this spawn point corresponds to
    private void Start()
    {
        string lastDoorUsed = SceneManagerScript.instance.GetLastDoorUsed();
        if (lastDoorUsed == "" || lastDoorUsed.Equals(doorIdentifier))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = transform.position;
        }
    }
}
