using SchoolRPG.Interaction.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorScript : Interactable
{
    [SerializeField] private SceneEventChannel sceneEventChannel;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string doorIdentifier; // Unique ID for each door

    public override void OnInteract()
    {
        SceneManagerScript.instance.SetLastDoorUsed(doorIdentifier);
        sceneEventChannel.RaiseOnOpenDoorRequested(sceneToLoad);
    }
    public string GetDoorIdentifier()
    {
        return doorIdentifier;
    }
}
