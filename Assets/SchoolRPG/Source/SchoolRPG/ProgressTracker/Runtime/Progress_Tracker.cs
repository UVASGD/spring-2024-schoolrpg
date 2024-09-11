using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SchoolRPG.ProgressTracker.Runtime
{
    /// <summary>
    /// Represents player progress. 
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ProgressTracker), menuName = "ScriptableObjects/ProgressTracker/ProgressTracker")]
    public class Progress_Tracker : ScriptableObject
    {
        /// <summary>
        /// The list of inventory items. 
        /// </summary>
        public bool[] tracker;

        public bool[] levelTracker;

    }
}
