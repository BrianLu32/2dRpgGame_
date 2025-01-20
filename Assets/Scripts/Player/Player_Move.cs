using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] 
    float moveSpeed = 5f;
    float speedX, speedY;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed; // Left < 0; Right > 0
        if(speedX < 0) {
            spriteRenderer.flipX = false;
        }
        else if(speedX > 0) {
            spriteRenderer.flipX = true;
        }
        speedY = Input.GetAxisRaw("Vertical") * moveSpeed;

        // Animation Controller
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(speedX));
        animator.SetFloat("VerticalSpeed", Mathf.Abs(speedY));

        print(speedX + " + " + speedY);
        rb.linearVelocity = new Vector2(speedX, speedY);
    }
}
