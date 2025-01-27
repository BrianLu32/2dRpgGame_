using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // General Movement
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float sprintMultiplyer = 2.0f;
    float speedX, speedY;

    // Dodge Mechanics
    [SerializeField] float dodgeCooldown = 2.0f;
    bool isDodgeCooldown = false;

    // Animation Controls
    public List<MovementSO> movement;
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

            if(Input.GetKey(KeyCode.LeftShift) && (Mathf.Abs(speedX) > 0 || Mathf.Abs(speedY) > 0))
            {
                Sprint();
                speedX *= sprintMultiplyer;
                speedY *= sprintMultiplyer;
            }
            else
            {
                animator.SetBool("isSprinting", false);
            }

            // Animation Controller
            animator.SetFloat("HorizontalSpeed", Mathf.Abs(speedX));
            animator.SetFloat("VerticalSpeed", Mathf.Abs(speedY));

            rb.linearVelocity = new Vector2(speedX, speedY);
        }
        else 
        {
            rb.linearVelocity = new Vector2(0.0f, 0.0f);
        }

        if(Input.GetKeyDown(KeyCode.Space) && !isDodgeCooldown)
        {
            Dodge();
        }
    }

    void Sprint()
    {
        animator.runtimeAnimatorController = movement[0].animatorOV; // 0 = sprint animation, should probably find a different way to handle this lol
        animator.SetBool("isSprinting", true);
        animator.Play("Sprint", 0);
    }

    void Dodge()
    {
        isDodgeCooldown = true;
        StartCoroutine(DodgeCooldownTimer());
        animator.runtimeAnimatorController = movement[1].animatorOV; // 1 = slide animation
        animator.Play("Slide", 0);
    }

    IEnumerator DodgeCooldownTimer()
    {
        yield return new WaitForSeconds(dodgeCooldown);
        isDodgeCooldown = false;
    }
}
