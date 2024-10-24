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
        Debug.Log("Door Interacted");
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        AudioClip audioClip = (AudioClip)Resources.Load<AudioClip>("Audio/Sound/creaky_door_open");
        Debug.Log(audioClip);
        audio.PlayOneShot(audioClip);
        
        SceneManagerScript.instance.SetLastDoorUsed(doorIdentifier);
        sceneEventChannel.RaiseOnOpenDoorRequested(sceneToLoad);
    }
    public string GetDoorIdentifier()
    {
        return doorIdentifier;
    }
}
