using System.Collections;
using TMPro;
using UnityEngine;

namespace SchoolRPG.Dialogue.Runtime
{
    public class Dialogue : MonoBehaviour
    {

        public TextMeshProUGUI textComponent;
        public string[] lines;
        public float textSpeed;

        private GameObject canvasParent;
        private Canvas dialogueCanvas;
        private int index;

        // Start is called before the first frame update
        void Start()
        {
            // get Canvas (parent) reference and disable its Canvas
            canvasParent = gameObject.transform.parent.gameObject;
            dialogueCanvas = canvasParent.GetComponent<Canvas>();
            dialogueCanvas.enabled = false;
            textComponent.text = string.Empty;
        }

        // Update is called once per frame
        void Update()
        {
            if (dialogueCanvas.enabled) {
                if (UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetKeyDown("space"))
                {
                    if (textComponent.text == lines[index])
                    {
                        NextLine();
                    }
                    else
                    {
                        // skip to finishing the current line
                        StopAllCoroutines();
                        textComponent.text = lines[index];
                    }
                }
            }
        }

        // trigger function from interactable script, pass lines
        public void StartDialogue(string[] objLines)
        {
            dialogueCanvas.enabled = true;
            lines = objLines;
            index = 0;
            StartCoroutine(TypeLine());
        }

        public IEnumerator TypeLine()
        {
            foreach (char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }

        public void NextLine()
        {
            if (index < lines.Length - 1)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            } 
            else
            {
                textComponent.text = string.Empty;
                dialogueCanvas.enabled = false;
                Debug.Log("DONE with lines, disabled canvas");
                //gameObject.SetActive(false);
            }
        }
    }
}
