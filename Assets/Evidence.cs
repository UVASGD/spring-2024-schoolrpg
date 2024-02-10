using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence : MonoBehaviour
{

    public string[] objLines;
    private GameObject dialogueBox;
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = GameObject.FindWithTag("DialogueBox");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EvidenceObserved()
    {
        Debug.Log("Evidence Observed");
        
        if (dialogueBox == null)
        {
            Debug.Log("Dialogue Box Missing");
        } else
        {
            dialogueBox.GetComponent<Dialogue>().StartDialogue(objLines);
        }

    }
}
