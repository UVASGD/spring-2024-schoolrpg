using System;
using System.Collections.Generic;
using SchoolRPG.Character.Runtime;
using SchoolRPG.Dialogue.Runtime;
using SchoolRPG.Interaction.Runtime;

using UnityEngine;

/// <summary>
/// Just raises dialogue event I'm not naming this rn
/// </summary>
public class TalkScript : Interactable
{
    public string[] dialogue;

    [SerializeField]
    protected DialogueEventChannel dialogueEventChannel;

    [SerializeField]
    protected CharacterMovementComponent playerMovement;

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
        Debug.Log("Talking triggered");
        dialogueEventChannel.RaiseOnOpenDialogueRequested(dialogue);
    }

    protected void EnableInteraction()
    {
        Debug.Log("Activated");
        isInteractable = true;
        playerMovement.Activate();
    }

    protected void DisableInteraction(IList<string> _ = null)
    {
        Debug.Log("Deactivated");
        isInteractable = false;
        playerMovement.Deactivate();
    }
}