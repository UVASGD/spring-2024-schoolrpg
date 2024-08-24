using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SchoolRPG.NPC.Runtime;
using UnityEditor.Experimental.GraphView;

public class NPCTalkScript : TalkScript
{
    [SerializeField]
    private NPC npc;

    public string[] passedDialogue;

    public override void Start()
    {
        base.Start();
    }

    public override void OnInteract()
    {
        if (!isInteractable) return;
        if (npc.IsPassed) dialogueEventChannel.RaiseOnOpenDialogueRequested(passedDialogue);
        else dialogueEventChannel.RaiseOnOpenDialogueRequested(dialogue);
        DisableInteraction();
    }
}