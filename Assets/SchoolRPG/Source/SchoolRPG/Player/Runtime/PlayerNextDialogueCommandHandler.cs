using System;
using SchoolRPG.CommandPattern.Runtime;
using SchoolRPG.Dialogue.Runtime;
using SchoolRPG.Input.Runtime;
using UnityEngine;

namespace SchoolRPG.Player.Runtime
{
    public class PlayerNextDialogueCommandHandler: MonoBehaviour
    {
        [SerializeField]
        private InputEventChannel inputEventChannel;

        [SerializeField] 
        private DialogueEventChannel dialogueEventChannel;
        
        private void OnEnable()
        {
            inputEventChannel.OnInteract += HandleOnInteract;
        }
    
        private void OnDisable()
        {
            inputEventChannel.OnInteract -= HandleOnInteract; 
        }

        private void HandleOnInteract()
        {
            var playerNextDialogueCommand = new PlayerNextDialogueCommand(dialogueEventChannel); 
            CommandInvoker.Execute(playerNextDialogueCommand);
        }
    }
}