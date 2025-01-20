using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    Animator animator;
    float cooldownTime = 2f;
    float nextFireTime = 0f;
    int numOfClicks = 0;
    float lastClickTime = 0f;
    float maxComboDelay = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    void OnClick()
    {
        lastClickTime = Time.time;
        numOfClicks++;
        if(numOfClicks == 1)
        {
            animator.SetBool("Lance_Attack_0", true);
        }
        numOfClicks = Mathf.Clamp(numOfClicks, 0, 3);
    }
}
