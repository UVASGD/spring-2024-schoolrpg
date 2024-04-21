using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTest : MonoBehaviour //Tests for: NPC (Cracked Skin), CARROT
{
    [SerializeField] private NPCScriptableObject npcScriptableObject;
    //[SerializeField] private PuzzleScriptableObject puzzleScriptableObject;
    private Player player;

    void Start() {
        // Debug.Log(npcScriptableObject.myName);
        // Debug.Log(npcScriptableObject.item);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        player = col.GetComponent<Player>();
        if(col.gameObject.tag == "Player")
        {
            Debug.Log(npcScriptableObject.myName);
            Debug.Log(npcScriptableObject.item);
            //Deug.Log(puzzleScriptableObject.completion[npcScriptableObject.id]);
        }
        // if (player.inventory.Contains(CollectableType.CARROT)) { //if (player.inventory.Contains(npcScriptableObject.item
        //     Debug.Log("Carrot in possession.");
        // }
    }
}
