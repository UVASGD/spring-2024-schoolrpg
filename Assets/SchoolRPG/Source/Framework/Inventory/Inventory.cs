using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{

    [System.Serializable]
    public class Slot
    {
        public CollectableType type;
        public Sprite icon;

        public Slot()
        {
            type = CollectableType.NONE;
        }

        public void AddItem(Interactable item)
        {
            this.type = item.type;
            this.icon = item.icon;
        }
    }

    public List<Slot> slots = new List<Slot>();
    
    public Inventory()
    {
        for(int i = 0; i < 10; i++)
        {
            slots.Add(new Slot());
        }
    }

    public void Add(Interactable item)
    {
        foreach(Slot slot in slots)
        {
            if(slot.type == CollectableType.NONE)
            {
                Debug.Log("Add-if works");
                slot.AddItem(item);
                Debug.Log("type" + slot.type);
                return;
            }
        }
    }

    public string toString()
    {
        string str = "";
        foreach(Slot slot in slots)
            if(slot.type != CollectableType.NONE)
                str += slot.type + " ";
        return str;
    }
}
