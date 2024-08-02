using SchoolRPG.Inventory.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcItemBehavior : MonoBehaviour
{
    public Inventory inventory;
    public List<InventoryItem> requiredItems;
    public GameObject passIndicator; // Some sort of icon to indicate the NPC has accepted the player's items

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (HasRequiredItems())
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

    private bool HasRequiredItems()
    {
        foreach(InventoryItem item in requiredItems)
        {
            if (!inventory.inventoryItems.Contains(item))
            {
                return false;
            }
        }
        return true;
    }
}
