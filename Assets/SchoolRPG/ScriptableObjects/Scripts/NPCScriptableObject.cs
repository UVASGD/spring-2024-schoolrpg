using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCScriptableObject", menuName = "ScriptableObjects/Scripts/NPCScriptableObject")]
public class NPCScriptableObject : ScriptableObject
{
   public string myName;
   public int id;
   public int floor;
   public CollectableType item; // Type?
}
