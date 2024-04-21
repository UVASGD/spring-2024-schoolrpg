using System;
using System.Collections.Generic;
using MEC;
using SchoolRPG.Source.SchoolRPG.PauseSystem.Runtime;
using TMPro;
using UnityEngine;

namespace SchoolRPG.Dialogue.Runtime
{
    /// <summary>
    /// Handles non-branching dialogue.
    /// </summary>
    public class DialogueHandler : MonoBehaviour
    {
        /// <summary>
        /// The <see cref="Canvas"/> containing the dialogue UI.
        /// </summary>
        [SerializeField]
        private Canvas dialogueCanvas;
        
        /// <summary>
        /// The text within the <see cref="dialogueCanvas"/> that displays the
        /// actual dialogue text.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI textComponent;
        
        [SerializeField]
        private DialogueEventChannel dialogueEventChannel;

        [SerializeField] 
        private PauseEventChannel pauseEventChannel;
        
        /// <summary>
        /// The dialogue sections. 
        /// </summary>
        private IList<string> dialogue;

        /// <summary>
        /// The index of current section of dialogue. 
        /// </summary>
        private int dialogueIndex; 
        
        /// <summary>
        /// The delay in seconds before showing the next character to the screen. 
        /// </summary>
        public float defaultTextDelay = 0.01f;

        private const string DialogueCoroutineTag = "Dialogue";

        /// <summary>
        /// Whether or not the dialogue had been completely typed out.
        /// </summary>
        private bool isDialogueComplete;

        private void OnEnable()
        {
            dialogueEventChannel.OnOpenDialogueRequested += OpenDialogue;
            dialogueEventChannel.OnNextDialogueRequested += NextDialogue;
            dialogueEventChannel.OnCloseDialogueRequested += CloseDialogue;
        }

        private void OnDisable()
        {
            dialogueEventChannel.OnOpenDialogueRequested -= OpenDialogue;
            dialogueEventChannel.OnNextDialogueRequested -= NextDialogue;
            dialogueEventChannel.OnCloseDialogueRequested -= CloseDialogue;
        }
        
        private void Start()
        {
            CloseDialogue();
        }

        private void OpenDialogue(IList<string> targetDialogue)
        {
            dialogueIndex = 0;
            dialogue = targetDialogue;
            textComponent.text = string.Empty;
            dialogueCanvas.enabled = true;
            isDialogueComplete = true;
            
            // NextDialogue();
            dialogueEventChannel.RaiseOnOpenDialogueCompleted();
            pauseEventChannel.RequestOnPauseStateChanged(PauseState.Partial);
        }
        
        private void NextDialogue()
        {
            if (!dialogueCanvas.enabled || dialogue == null) return;

            if (!isDialogueComplete)
            {
                Timing.KillCoroutines(DialogueCoroutineTag);
                OnCompleteDialogue();
                Debug.Log(dialogueIndex);
            }

            if (dialogueIndex < 0 || dialogueIndex >= dialogue.Count)
            {
                CloseDialogue();
                return;
            }
            
            isDialogueComplete = false;
            textComponent.text = string.Empty;
            
            Timing.RunCoroutine(_StartDialogue(dialogue[dialogueIndex]), DialogueCoroutineTag);
        }
        
        private IEnumerator<float> _StartDialogue(string targetDialogue)
        {
            foreach (var character in targetDialogue)
            {
                textComponent.text += character;
                yield return Timing.WaitForSeconds(defaultTextDelay);
            }
            
            OnCompleteDialogue();
        }

        private void OnCompleteDialogue()
        {
            isDialogueComplete = true;
            dialogueEventChannel.RaiseOnNextDialogueCompleted(); 
            dialogueIndex++;
        }
        
        private void CloseDialogue()
        {
            dialogueCanvas.enabled = false;
            dialogue = Array.Empty<string>();
            dialogueIndex = -1; 
            
            dialogueEventChannel.RaiseOnCloseDialogueCompleted();
            pauseEventChannel.RequestOnPauseStateChanged(PauseState.NotPaused);
        }
    }
}
