using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SchoolRPG.NPC.Runtime;
using UnityEditor.Experimental.GraphView;
using SchoolRPG.Interaction.Runtime;

public class NPCTalkScript : TalkScript
{

    [SerializeField]
    private NPC npc;

    public override void Start()
    {
        base.Start();
    }

    public override void OnInteract()
    {
        Debug.Log("Attempt NPC interact");
        if (!isInteractable) return;
        if (npc.IsPassed) dialogueEventChannel.RaiseOnOpenDialogueRequested(npc.AlreadyPassedDialogue);
        else dialogueEventChannel.RaiseOnOpenDialogueRequested(npc.Dialogue);
        DisableInteraction();
    }
}