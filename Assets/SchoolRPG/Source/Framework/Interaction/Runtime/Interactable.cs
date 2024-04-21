using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    bool inside = false;
    public UnityEvent interactAction;
    private Canvas dialogueCanvas;
    private bool inDialogue; // make this a STATE?
    
    // for collecting
    public bool isCollectable;
    public CollectableType type;
    private Player player;
    public Sprite icon;

    // Start is called before first update
    void Start()
    {
        dialogueCanvas = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        player = col.GetComponent<Player>();
        if(col.gameObject.tag == "Player")
        {
            inside = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            inside = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If inside interaction zone and dialogue has NOT STARTED, start dialogue.

        if (inside == true && Input.GetKeyDown(KeyCode.E)) 
        {
            // Debug.Log("collectable? " + isCollectable);
            if (!dialogueCanvas.enabled && !isCollectable)
            {
                Debug.Log("START DIALOGUE");
                interactAction.Invoke();
            }
            
        }
        if (inside == true && Input.GetKeyDown(KeyCode.P)){
            if(isCollectable)
            {
                // Debug.Log("collected");
                this.transform.parent.gameObject.SetActive(false);
                //this.transform.parent.gameObject.show()
                Debug.Log("this: " + this.type);
                player.inventory.Add(this);
            }
        }
         
        // the update has no way of knowing if the dialogue needs to end
    }
}

public enum CollectableType
{
    NONE, CARROT, BLIRBY, CIG
}