using SchoolRPG.Dialogue.Runtime;
using SchoolRPG.Input.Runtime;
using SchoolRPG.Inventory.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcItemBehavior : MonoBehaviour
{
    public Inventory inventory;
    public InventoryItem requiredItem;
    public GameObject passIndicator; // Some sort of icon to indicate the NPC has accepted the player's items
    [SerializeField]
    private InventoryEventChannel InventoryEventChannel;
    [SerializeField]
    private InventoryManager InventoryManager;

    [SerializeField]
    private DialogueEventChannel dialogueEventChannel;

    [SerializeField]
    private InputEventChannel inputEventChannel;

    private InventoryItem selectedItem;
    private bool passed;
    public string[] passDialogue;

    private void Start()
    {
        Debug.Log("NPC item behavior activated");
        passed = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        selectedItem = InventoryManager.GetSelectedInventoryItem();
        
        if (other.CompareTag("Player"))
        {
            Debug.Log(selectedItem);
            if (selectedItem != null && selectedItem.Equals(requiredItem) && !passed)
            {
                //placeholder thing for npc allowing player to pass
                passed = true;
                // need to auto close inventory
                inputEventChannel.RaiseOnInventory();
                dialogueEventChannel.RaiseOnOpenDialogueRequested(passDialogue);
                // For some reason, an empty dialogue box appears 
                inputEventChannel.RaiseOnInteract();
                selectedItem = null;
                Debug.Log("Player has the required items for " + gameObject.name);
            }
            else
            {
                Debug.Log("Player does not have the required items for " + gameObject.name);
            }
        }
    }

}
