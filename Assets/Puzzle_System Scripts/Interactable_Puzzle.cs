using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable_Puzzle : MonoBehaviour
{
    bool inside = false;
    public UnityEvent interactAction;
    public GameObject puzzleScreen;
    private bool failSafe = false; // make this a STATE?

    // Start is called before first update
    // void Start()
    // {
    //     puzzleScreen = GameObject.Find("PuzzleScreenGroup");
    // }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            inside = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            inside = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If inside interaction zone and puzzle is NOT OPEN, open puzzle.

        if (inside == true && Input.GetKeyDown(KeyCode.X)) 
        {         
            if (!puzzleScreen.activeSelf && !failSafe)
            {
                Debug.Log("OPEN PUZZLE");
                failSafe = true;
                puzzleScreen.SetActive(true);
                StartCoroutine(FailSafe());
            }

            if (puzzleScreen.activeSelf && !failSafe)
            {
                Debug.Log("OPEN PUZZLE");
                failSafe = true;
                puzzleScreen.SetActive(false);
                StartCoroutine(FailSafe());
            }
        }

        IEnumerator FailSafe(){
            yield return new WaitForSeconds(0.25f);
            failSafe = false;
        }
         
        // the update has no way of knowing if the dialogue needs to end

    
    }



}