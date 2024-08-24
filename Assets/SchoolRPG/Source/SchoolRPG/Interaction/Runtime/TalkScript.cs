using System;
using System.Collections.Generic;
using SchoolRPG.Dialogue.Runtime;
using SchoolRPG.Interaction.Runtime;

using UnityEngine;

/// <summary>
/// Just raises dialogue event I'm not naming this rn
/// </summary>
public class TalkScript: Interactable
{
    public string[] dialogue;
    
    [SerializeField] 
    protected DialogueEventChannel dialogueEventChannel;

    protected bool isInteractable;

    private void OnEnable()
    {
        dialogueEventChannel.OnOpenDialogueRequested += DisableInteraction;
        dialogueEventChannel.OnCloseDialogueCompleted += EnableInteraction;
    }
    
    private void OnDisable()
    {
        dialogueEventChannel.OnOpenDialogueRequested -= DisableInteraction;
        dialogueEventChannel.OnCloseDialogueCompleted -= EnableInteraction;
    }

    public virtual void Start()
    {
        EnableInteraction();
    }

    public override void OnInteract()
    {
        if (!isInteractable) return;
        dialogueEventChannel.RaiseOnOpenDialogueRequested(dialogue);
        DisableInteraction();
    }

    protected void EnableInteraction()
    {
        isInteractable = true;
    }

    protected void DisableInteraction(IList<string> _ = null)
    {
        isInteractable = false;
    }
}
