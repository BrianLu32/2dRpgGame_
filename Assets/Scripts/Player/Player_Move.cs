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
        // Only move if the player stops attacking, aka spamming left click lol
        if(!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) 
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

            rb.linearVelocity = new Vector2(speedX, speedY);
        }
        else 
        {
            rb.linearVelocity = new Vector2(0.0f, 0.0f);
        }
    }
}
