using SchoolRPG.Dialogue.Runtime;
using SchoolRPG.Input.Runtime;
using SchoolRPG.Inventory.Runtime;
using SchoolRPG.NPC.Runtime;
using SchoolRPG.ProgressTracker.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcItemBehavior : MonoBehaviour
{
    public Inventory inventory;
    public ProgressTracker passIndicator;
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

    private InventoryItem dummyItem;

    private void Start()
    {
        npc.IsPassed = false;
        dummyItem = ScriptableObject.CreateInstance<InventoryItem>();
        Debug.Log("NPC item behavior activated");
    }

    private void OnEnable()
    {
        inputEventChannel.OnInventory += setIsInventoryOpened;
    }

    private void OnDisable()
    {
        inputEventChannel.OnInventory -= setIsInventoryOpened;
    }

    private void Update()
    {
        //Debug.Log(InventoryManager.GetSelectedInventoryItem());
    }

    private void setIsInventoryOpened()
    {
        isInventoryOpened = !isInventoryOpened;
        if (!isInventoryOpened)
        {
            InventoryEventChannel.RaiseOnSetSelectedInventoryItem(dummyItem);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        selectedItem = InventoryManager.GetSelectedInventoryItem();
        if (other.CompareTag("Player") && selectedItem != null && !selectedItem.Equals(dummyItem) && !npc.IsPassed)
        {
            if (selectedItem.Equals(npc.RequiredItem))
            {
                //placeholder thing for npc allowing player to pass
                npc.IsPassed = true;
                // need to auto close inventory
                inputEventChannel.RaiseOnInventory();
                inventory.inventoryItems.Remove(npc.RequiredItem);
                dialogueEventChannel.RaiseOnOpenDialogueRequested(npc.PassDialogue);
                // For some reason, an empty dialogue box appears 
                inputEventChannel.RaiseOnInteract();
            }
            else
            {
                inputEventChannel.RaiseOnInventory();
                dialogueEventChannel.RaiseOnOpenDialogueRequested(npc.NoPassDialogue);
                inputEventChannel.RaiseOnInteract();
            }
        }
    }
}