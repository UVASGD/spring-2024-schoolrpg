using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = nameof(NPC), menuName = "ScriptableObjects/NPC")]

public class NPC : ScriptableObject
{
    /// <summary>
    /// The name that represents this NPC. Also used as an id. 
    /// </summary>
    [field: SerializeField]
    public string Name {get; set;}

    /// <summary>
    /// The item that the NPC is assigned to and will accept from the player for the player to pass. 
    /// </summary>
    [field: SerializeField]
    public string AssignedItem {get; set;}

    /// <summary>
    /// The level that the NPC will show up in. 
    /// </summary>
    [field: SerializeField]
    public int Level {get; set;}

    /// <summary>
    /// Whether the NPC has or has not accepted its assigned item from the player.  
    /// </summary>
    [field: SerializeField]
    public bool IsPassed {get; set;}
}
