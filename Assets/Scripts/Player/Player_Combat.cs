using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    Animator animator;

    // Attack Mechanics
    public List<AttackSO> combo;
    public List<float> comboDelays;
    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;
    public float comboDelay = 0.0f;
    public float attackDelay = 0.2f;

    // Block Mechanics
    public AttackSO blockSO;
    bool isBlocking = false;
    [SerializeField] float blockCooldown = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if(Input.GetMouseButtonDown(1) && !isBlocking)
        {
            Block();
        }
        ExitAttack();
    }

    void Attack()
    {
        // Lower comboDelay = faster combo start, allowing more of spam clikcing
        if(Time.time - lastComboEnd > comboDelay && comboCounter < combo.Count)
        {
            CancelInvoke("EndCombo");

            if(Time.time - lastClickedTime >= comboDelays[comboCounter])
            {
                animator.runtimeAnimatorController = combo[comboCounter].animatorOV;
                animator.Play("Attack", 0, 0);
                // Add weapon damage manipulation here
                comboCounter++;
                lastClickedTime = Time.time;

                if(comboCounter > combo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            // Int for second argument forces the method to be called for that duration (if that makes sense)
            // Removes the forced seconds played from idle
            Invoke("EndCombo", 0);
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }

    void Block()
    {
        isBlocking = true;
        StartCoroutine(BlockCooldownTimer());
        animator.runtimeAnimatorController = blockSO.animatorOV;
        animator.Play("Block", 0, 0);
    }

    IEnumerator BlockCooldownTimer()
    {
        yield return new WaitForSeconds(blockCooldown);
        isBlocking = false;
    }
}
