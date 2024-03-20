using UnityEngine;
using UnityEngine.Events;

namespace SchoolRPG.Interaction.Runtime
{
    public class Interactable : MonoBehaviour
    {
        bool inside = false;
        public UnityEvent interactAction;
        private Canvas dialogueCanvas;
        private bool inDialogue; // make this a STATE?

        // Start is called before first update
        void Start()
        {
            dialogueCanvas = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
        }

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
            // If inside interaction zone and dialogue has NOT STARTED, start dialogue.

            if (inside == true && UnityEngine.Input.GetKeyDown(KeyCode.E)) 
            {         
                if (!dialogueCanvas.enabled)
                {
                    Debug.Log("START DIALOGUE");
                    interactAction.Invoke();
                }
            }
         
            // the update has no way of knowing if the dialogue needs to end
        }


    }
}
