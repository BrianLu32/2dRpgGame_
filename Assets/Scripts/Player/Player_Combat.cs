using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    Animator animator;

    public List<AttackSO> combo;
    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;
    public float comboDelay = 0.5f;
    public float attackDelay = 0.2f;

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
        ExitAttack();
    }

    void Attack()
    {
        if(Time.time - lastComboEnd > comboDelay && comboCounter < combo.Count)
        {
            CancelInvoke("EndCombo");

            if(Time.time - lastClickedTime >= attackDelay)
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
            Invoke("EndCombo", 1);
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
