namespace SchoolRPG.Source.SchoolRPG.PauseSystem.Runtime
{
    public enum PauseState
    {
        /// <summary>
        /// The game is not paused at all.
        /// </summary>
        NotPaused,
        
        /// <summary>
        /// The game is fully paused. Nothing should move. 
        /// </summary>
        Paused, 
        
        /// <summary>
        /// The game is partially paused (e.g. during a cinematic),
        /// and some things may move. 
        /// TODO - this is not very descriptive 
        /// </summary>
        Partial, 
    }
}