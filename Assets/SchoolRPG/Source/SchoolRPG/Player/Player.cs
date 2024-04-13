using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    
    void Awake()
    {
        inventory = new Inventory();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(inventory.toString());
        }    
    }
}
