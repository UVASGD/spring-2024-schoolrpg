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
    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private ProgressTracker progressTracker;

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
    private bool isInventoryOpened = false;

    private InventoryItem dummyItem;

    private void Start()
    {
        //npc.IsPassed = false;
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
      
    // Marks the progresstracker scriptableobject. 
    /*
     * Progress tracker is essentially like this: 0 - false; 1 - true
     * 0 - 0 - 0 - 0 - 0 - 0 - 0 - 0 - 0
     * Level 1 is the first three, level 2 is the second three, level 3 is the third three. 
     * This will set the item's respective id/number as true when called
     * Eventually, when the level is completed, all elements of that level will be true.
     */
    private void markTracker()
    {
        progressTracker.tracker[npc.RequiredItem.Id] = true;
    }

    // Check the whole level is complete when progress is made
    private bool levelPassed()
    {
        // Level 1 start: 0; Level 2 start: 3; Level 3 start: 6
        for (int index = (npc.Level - 1) * 3; index < index + 3; index++)
        {
            if (!progressTracker.tracker[index])
            {
                return false;
            }
        }
        return true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        selectedItem = InventoryManager.GetSelectedInventoryItem();
        if (other.CompareTag("Player") && selectedItem != null && !selectedItem.Equals(dummyItem) && !npc.IsPassed)
        {
            if (selectedItem.Equals(npc.RequiredItem))
            {
                // need to auto close inventory
                inputEventChannel.RaiseOnInventory();
                inventory.inventoryItems.Remove(npc.RequiredItem);
                dialogueEventChannel.RaiseOnOpenDialogueRequested(npc.PassDialogue);
                // For some reason, an empty dialogue box appears 
                inputEventChannel.RaiseOnInteract();
                //Passing the match, checking completion
                npc.IsPassed = true;
                markTracker();
                if (levelPassed())
                {
                    Debug.Log("You passed level " + npc.Level);
                }
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