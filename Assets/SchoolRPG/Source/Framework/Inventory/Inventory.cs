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

        public Slot()
        {
            type = CollectableType.NONE;
        }

        public void AddItem(CollectableType type)
        {
            this.type = type;
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

    public void Add(CollectableType typeToAdd)
    {
        foreach(Slot slot in slots)
        {
            if(slot.type == CollectableType.NONE)
            {
                slot.AddItem(typeToAdd);
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
