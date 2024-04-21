using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public PuzzleScriptableObject puzzle;
    
    void Awake()
    {
        inventory = new Inventory();
        //puzzle = new PuzzleScriptableObject();
            //PuzzleScriptableObject must be instantiated using the ScriptableObject.CreateInstance method instead of new PuzzleScriptableObject.


    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(inventory.toString());
        }    
    }
}
