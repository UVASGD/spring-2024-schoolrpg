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

    private AudioClip doorOpen1;
    private AudioClip doorOpen2;


    private void Start()
    {
        doorOpen1 = Resources.Load<AudioClip>("Audio/Sound/creaky_door_open");
        doorOpen2 = Resources.Load<AudioClip>("Audio/Sound/door_open_3");
        Debug.Log(doorOpen1);
        Debug.Log(doorOpen2);
    }

    public override void OnInteract()
    {
        Debug.Log("Door Interacted");
        AudioSource audio = gameObject.GetComponent<AudioSource>();

        int rand = new System.Random().Next(0,2);
        if (rand == 0)
        {
            Debug.Log(doorOpen1 == null);

            audio.PlayOneShot(doorOpen1);
        }
        else
            audio.PlayOneShot(doorOpen2);
        
        SceneManagerScript.instance.SetLastDoorUsed(doorIdentifier);
        sceneEventChannel.RaiseOnOpenDoorRequested(sceneToLoad);
    }
    public string GetDoorIdentifier()
    {
        return doorIdentifier;
    }
}
