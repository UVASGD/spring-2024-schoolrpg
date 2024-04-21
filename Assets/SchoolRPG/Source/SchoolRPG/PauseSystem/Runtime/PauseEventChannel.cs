using System;
using UnityEngine;

namespace SchoolRPG.Source.SchoolRPG.PauseSystem.Runtime
{
    /// <summary>
    /// Channel over which to communicate when certain game systems should halt execution. 
    /// </summary>
    [CreateAssetMenu(fileName = nameof(PauseEventChannel), menuName = "ScriptableObjects/Pause System/Pause Event Channel")]
    public class PauseEventChannel : ScriptableObject
    {
        /// <summary>
        /// Callback to when the <see cref="PauseState"/> is changed. 
        /// </summary>
        public Action<PauseState> onPauseStateChanged;

        /// <summary>
        /// The last set <see cref="PauseState"/>. 
        /// </summary>
        public PauseState PauseState { get; private set; } = PauseState.NotPaused;

        /// <summary>
        /// Invokes <see cref="onPauseStateChanged"/>, and changes the <see cref="PauseState"/>. 
        /// </summary>
        public void RequestOnPauseStateChanged(PauseState newPauseState)
        {
            PauseState = newPauseState;
            onPauseStateChanged?.Invoke(newPauseState);
        }
    }
}