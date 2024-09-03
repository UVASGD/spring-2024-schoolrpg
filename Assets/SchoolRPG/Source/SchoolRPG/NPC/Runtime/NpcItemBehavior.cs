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
      
    // Marks the progresstracker scriptableobject. 
    /*
     * Progress tracker is essentially like this: 0 - false 1 - true
     * 0 - 0 - 0 - 0 - 0 - 0 - 0 - 0 - 0
     * Level 1 is the first three, level 2 is the second three, level 3 is the third three. This method will set the first false it sees to true in the corresponding level. Eventually, when the level is completed, all elements of that level will be true
     */
    private void markTracker()
    {
        for(int i = npc.Level * 3 + 1; i < npc.Level * 3 + 3; i++)
        {
            if (!progressTracker.tracker[i])
                progressTracker.tracker[i] = true;
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
                markTracker();
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