using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slots_UI : MonoBehaviour
{
    public Image itemIcon;
    
    public void SetItem(Inventory.Slot slot)
    {
        if(slot != null)
        {
            Debug.Log("set item not null");
            // Debug.Log(slot.icon);
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1,1,1,1);
        }
        else{
            Debug.Log("null item");
        }
    }

    public void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1,1,1,0);
    }
}
