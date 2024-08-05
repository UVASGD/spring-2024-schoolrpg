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
    public InventoryEventChannel InventoryEventChannel;
    public InventoryManager InventoryManager;

    private InventoryItem selectedItem;

    private void Start()
    {
        Debug.Log("NPC item behavior activated");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        selectedItem = InventoryManager.GetSelectedInventoryItem();
        
        if (other.CompareTag("Player"))
        {
            Debug.Log(selectedItem);
            if (selectedItem != null && selectedItem.Equals(requiredItem))
            {
                //placeholder thing for npc allowing player to pass
                Debug.Log("Player has the required items for " + gameObject.name);
            }
            else
            {
                Debug.Log("Player does not have the required items for " + gameObject.name);
            }
        }
    }

}
