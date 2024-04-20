using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleObject", menuName = "ScriptableObjects/PuzzleObject")]
public class PuzzleScriptableObject : ScriptableObject
{
    public bool[] completion = new bool[9];
    public bool level1; // Level as scriptable object? Number, locked status, completion, NPC(?) --> Only thinking about this bc there seems to be a lot of different level traits I'm calling on.
    public bool level2;
    public bool level3;

    public void checkFloor(int lvl) { // Unsure how level number will be accessed.
        // bool lvl1 = false;
        // bool lvl2 = false;
        // bool lvl3 = false;

        if (lvl == 1) { 
            if (completion[0] == true && completion[1] == true && completion[2] == true) {
                level1 = true;
                unlockFloor(2);
                Debug.Log("Level 1 cleared.");
            }
        } 
        if (lvl == 2 && level1 == true) { // Previous levels must be completed; might be redundant.
            if (completion[3] == true && completion[4] == true && completion[5] == true) {
                level2 = true;
                unlockFloor(3);
                Debug.Log("Level 2 cleared.");
            }
        }
        if (lvl == 3 && level1 == true && level2 == true) {
            if (completion[3] == true && completion[4] == true && completion[5] == true) {
                level3 = true;
                Debug.Log("Level 2 cleared.");
            }
        }
    }

    public void unlockFloor(int lvl) { // Inefficient lol
        if (lvl == 2 && level1 == true) {
            Debug.Log("Level 2 unlocked.");
        }
        if (lvl == 3 && level1 == true && level2 == true) {
            Debug.Log("Level 3 unlocked.");
        }
    }
}
