using System;
using System.Collections;
using System.Collections.Generic;
using SchoolRPG.Dialogue.Runtime;
using SchoolRPG.Input.Runtime;
using UnityEngine;

/// <summary>
/// Just raises dialogue event I'm not naming this rn
/// </summary>
public class PlayerTalkScript: MonoBehaviour
{
    
    public string[] blurb1;
    public string[] blurb2;
    public string[] blurb3;
    private int monologueCounter;
    private List<string[]> monologues;

    [SerializeField] 
    private DialogueEventChannel dialogueEventChannel;

    [SerializeField]
    private InputEventChannel inputEventChannel;
    

    // for some reason, start() will not make the dialogue visible until a small delay is implemented
    private void Start()
    {
        monologues = new List<string[]>();
        monologues.Add(blurb1);
        monologues.Add(blurb2);
        monologues.Add(blurb3); 
    }

    public void TriggerDialogueCoroutine()
    {
        StartCoroutine(TriggerDialogueAfterDelay());
    }

    private IEnumerator TriggerDialogueAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        
        dialogueEventChannel.RaiseOnOpenDialogueRequested(monologues[monologueCounter]);
        inputEventChannel.RaiseOnInteract();
        monologueCounter++;
    }


}
