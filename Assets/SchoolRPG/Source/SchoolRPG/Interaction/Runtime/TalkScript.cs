using System.Collections.Generic;
using SchoolRPG.Dialogue.Runtime;
using UnityEngine;

namespace SchoolRPG.Interaction.Runtime
{
    /// <summary>
    /// Just raises dialogue event I'm not naming this rn
    /// </summary>
    public class TalkScript: Interactable
    {
        
        public string[] dialogue;

        [SerializeField] 
        private DialogueEventChannel dialogueEventChannel;

        private bool isInteractable;

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

        private void Start()
        {
            EnableInteraction();
        }

        public override void OnInteract()
        {
            if (!isInteractable) return;
            dialogueEventChannel.RaiseOnOpenDialogueRequested(dialogue);
            DisableInteraction();
        }

        private void EnableInteraction()
        {
            isInteractable = true;
        }

        private void DisableInteraction(IList<string> _ = null)
        {
            isInteractable = false;
        }
    }
}