using System;
using System.Collections.Generic;
using UnityEngine;

namespace SchoolRPG.Dialogue.Runtime
{
    /// <summary>
    /// Event channel for dialogue events. 
    /// </summary>
    [CreateAssetMenu(fileName = nameof(DialogueEventChannel), menuName = "ScriptableObjects/Dialogue/Dialogue Event Channel", order = 0)]
    public class DialogueEventChannel : ScriptableObject
    {
        /// <summary>
        /// Callback when the dialogue should start to be opened and written to for the first time.
        /// </summary>
        public event Action<IList<string>> OnOpenDialogueRequested;

        /// <summary>
        /// Callback when the dialogue is fully opened. 
        /// </summary>
        public event Action OnOpenDialogueCompleted;

        /// <summary>
        /// Callback to write text to the opened dialogue. 
        /// </summary>
        public event Action OnNextDialogueRequested;

        /// <summary>
        /// Callback when the dialogue has been written. 
        /// </summary>
        public event Action OnNextDialogueCompleted;

        /// <summary>
        /// Callback on when close dialogue box should start to be closed. 
        /// </summary>
        public event Action OnCloseDialogueRequested;

        /// <summary>
        /// Callback when the dialogue box is completely closed. 
        /// </summary>
        public event Action OnCloseDialogueCompleted;
        
        /// <inheritdoc cref="OnOpenDialogueRequested"/>
        /// <param name="dialogue">The dialogue to be written. Each element represents one text box.</param>
        public void RaiseOnOpenDialogueRequested(IList<string> dialogue) => OnOpenDialogueRequested?.Invoke(dialogue);
        
        /// <inheritdoc cref="OnOpenDialogueCompleted"/>
        public void RaiseOnOpenDialogueCompleted() => OnOpenDialogueCompleted?.Invoke();

        /// <inheritdoc cref="OnNextDialogueRequested"/>
        public void RaiseOnNextDialogueRequested()
        {
            OnNextDialogueRequested?.Invoke();
        } 

        /// <inheritdoc cref="OnNextDialogueCompleted"/>
        public void RaiseOnNextDialogueCompleted() => OnNextDialogueCompleted?.Invoke();

        /// <inheritdoc cref="OnCloseDialogueRequested"/>
        public void RaiseOnCloseDialogueRequested() => OnCloseDialogueRequested?.Invoke();

        /// <inheritdoc cref="OnCloseDialogueCompleted"/>
        public void RaiseOnCloseDialogueCompleted() => OnCloseDialogueCompleted?.Invoke();
    }
}