using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show : MonoBehaviour
{
    [SerializeField] private NPCScriptableObject npc;
    [SerializeField] private PuzzleScriptableObject puzzle; // Should not be created for each NPC tho...maybe in awake() in Player?
    bool inside = false;
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        player = col.GetComponent<Player>();
        if(col.gameObject.tag == "Player")
        {
            inside = true;
            Debug.Log("inside");
            Debug.Log(npc.item);
            //show(npc);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            inside = false;
        }
    }

    void Update()
    {
        if (inside == true && Input.GetKeyDown(KeyCode.C)) { // KeyDown not working?
            //Debug.Log("here2");
            Debug.Log(npc.item);
            if(player.inventory.Contains(npc.item)) { // CollectableType.Carrot
                Debug.Log("NPC item in possession.");
                Debug.Log(npc.id); //pass
                Debug.Log("BEFORE: " + puzzle.completion[npc.id]); //wait uhhh after getting it once its true always how to reset lol
                puzzle.completion[npc.id] = true;
                Debug.Log("AFTER: " + puzzle.completion[npc.id]);
                puzzle.checkFloor(npc.floor);
            }
        }
    }
}
