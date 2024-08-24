using SchoolRPG.Dialogue.Runtime;
using SchoolRPG.Input.Runtime;
using SchoolRPG.Inventory.Runtime;
using SchoolRPG.NPC.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NpcItemBehavior : MonoBehaviour
{
    public Inventory inventory;
    public GameObject passIndicator; // Some sort of icon to indicate the NPC has accepted the player's items
    [SerializeField]
    private InventoryEventChannel InventoryEventChannel;
    [SerializeField]
    private InventoryManager InventoryManager;

    [SerializeField]
    private NPC npc;

    [SerializeField]
    private DialogueEventChannel dialogueEventChannel;

    [SerializeField]
    private InputEventChannel inputEventChannel;

    private InventoryItem selectedItem;
    private bool isInventoryOpened;

    private void Start()
    {
        npc.IsPassed = false;
        Debug.Log("NPC item behavior activated");
    }

    private void OnEnable(){
        inputEventChannel.OnInventory += setIsInventoryOpened;
    }

    private void OnDisable(){
        inputEventChannel.OnInventory -= setIsInventoryOpened;
    }

    private void setIsInventoryOpened(){
        isInventoryOpened = !isInventoryOpened;
        if (!isInventoryOpened){
            InventoryEventChannel.RaiseOnSetSelectedInventoryItem(ScriptableObject.CreateInstance<InventoryItem>());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        selectedItem = InventoryManager.GetSelectedInventoryItem();

        if (other.CompareTag("Player"))
        {
            Debug.Log(selectedItem);
            if (selectedItem != null && !selectedItem.Equals(ScriptableObject.CreateInstance<InventoryItem>()) && !npc.IsPassed){
                if(selectedItem.Equals(npc.RequiredItem)){
                    //placeholder thing for npc allowing player to pass
                    npc.IsPassed = true;
                    // need to auto close inventory
                    inputEventChannel.RaiseOnInventory();
                    inventory.inventoryItems.Remove(npc.RequiredItem);
                    dialogueEventChannel.RaiseOnOpenDialogueRequested(npc.PassDialogue);
                    // For some reason, an empty dialogue box appears 
                    inputEventChannel.RaiseOnInteract();
                    selectedItem = null;
                }
                else{
                    dialogueEventChannel.RaiseOnOpenDialogueRequested(npc.NoPassDialogue);
                    inputEventChannel.RaiseOnInteract();
                }
            }
        }
    }

}
