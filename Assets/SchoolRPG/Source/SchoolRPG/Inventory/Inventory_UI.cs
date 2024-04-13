using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Player player;
    public List<Slots_UI> slots = new List<Slots_UI>();
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void Awake()
    {
        inventoryPanel.SetActive(false);
    }

    public void ToggleInventory()
    {
        if(!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            Setup();
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }
    
    void Setup()
    {
        Debug.Log("Setup");
        Debug.Log("inventory: " + player.inventory.toString());
        for(int i = 0; i < 10; i++)
        {
            if(player.inventory.slots[i].type != CollectableType.NONE)
            {
                
                slots[i].SetItem(player.inventory.slots[i]);
            }
            else
            {
                // Debug.Log("collectable is none");
                slots[i].SetEmpty();
            }
        }
    }
}
