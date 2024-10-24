using System;
using System.Collections;
using System.Collections.Generic;
using SchoolRPG.Dialogue.Runtime;
using SchoolRPG.Character.Runtime;
using SchoolRPG.Input.Runtime;
//using UnityEditor.Build;
using UnityEngine;

/// <summary>
/// Just raises dialogue event I'm not naming this rn
/// </summary>
public class PlayerTalkScript : MonoBehaviour // honestly, no need to extend talkscript
{
    [SerializeField]
    public string[] monologue;

    [SerializeField]
    private int lineCount = 0;

    [SerializeField]
    private DialogueEventChannel dialogueEventChannel;
    /*
    [SerializeField]
    protected CharacterMovementComponent playerMovement;*/

    [SerializeField]
    private InputEventChannel inputEventChannel;
    
    /*
    private void OnEnable()
    {
        dialogueEventChannel.OnOpenDialogueRequested += DisableMovement;
        dialogueEventChannel.OnCloseDialogueCompleted += EnableMovement;
    }

    private void OnDisable()
    {
        dialogueEventChannel.OnOpenDialogueRequested -= DisableMovement;
        dialogueEventChannel.OnCloseDialogueCompleted -= EnableMovement;
    }*/
    

    // for some reason, start() will not make the dialogue visible until a small delay is implemented
    private void Start()
    {
        //StartCoroutine(TriggerDialogueAfterDelay());
        //playerMovement.Deactivate();
    }
    
    private IEnumerator TriggerDialogueAfterDelay()
    {
        // modify to do line by line --> timed dialogue
        yield return new WaitForSeconds(0.1f);
        string[] newLine = { monologue[lineCount] };
        dialogueEventChannel.RaiseOnOpenDialogueRequested(newLine); // make array of 
        inputEventChannel.RaiseOnInteract();
        lineCount += 1;
    }

    public void CloseMonologue()
    {
        dialogueEventChannel.RaiseOnCloseDialogueRequested();
    }

    public void StartMonologue()
    {
        Debug.Log("monologue triggered");
        StartCoroutine(TriggerDialogueAfterDelay());
    }
    /*
    private void EnableMovement()
    {
        playerMovement.Activate();
    }

    private void DisableMovement(IList<string> _ = null)
    {
        playerMovement.Deactivate();
    }*/


}