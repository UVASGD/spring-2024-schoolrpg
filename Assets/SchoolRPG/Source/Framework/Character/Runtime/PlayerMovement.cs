using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 2f;
    private Canvas dialogueCanvas;

    public Rigidbody2D rb;

    Vector2 mov;

    //called before first update
    void Start()
    {
        dialogueCanvas = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        mov.x = Input.GetAxisRaw("Horizontal");
        mov.y = Input.GetAxisRaw("Vertical");
    }

    // execute on fixed timer, better for physics (default 50 times/sec)
    void FixedUpdate()
    {
        // if not in dialogue, allow movement.
        if (!dialogueCanvas.enabled)
        {
            rb.MovePosition(rb.position + mov.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
}