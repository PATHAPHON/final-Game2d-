using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackControls : MonoBehaviour
{
    private PlayerMoveControls playerMoveControlss;
    private GatherInput1 gatherInput;
    private Animator animator;

    public PolygonCollider2D attackCollider;

    public bool attackStarted = false;
    public float attackDelay = 1.0f; // ตั้งดีเลย์ระหว่างการโจมตีแต่ละครั้ง
    private bool canAttack = true;

    void Start()
    {
        playerMoveControlss = GetComponent<PlayerMoveControls>();
        gatherInput = GetComponent<GatherInput1>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canAttack) // ตรวจสอบว่าพร้อมโจมตีหรือไม่
        {
            Attack();
        }
    }

    private void Attack()
    {
        // ตรวจสอบว่ากดปุ่มโจมตี
        if (gatherInput.tryAttack)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        canAttack = false; // ป้องกันไม่ให้โจมตีซ้ำจนกว่าจะถึงดีเลย์
        attackStarted = true;

        animator.SetBool("Attack", true); // ตั้งค่าให้เริ่มอนิเมชันการโจมตี
        yield return new WaitForSeconds(0.1f); // รอเล็กน้อยให้อนิเมชันเริ่มต้น

        animator.SetBool("Attack", false); // รีเซ็ตการโจมตีหลังจากอนิเมชันทำงาน
        yield return new WaitForSeconds(attackDelay); // รอเป็นเวลาที่กำหนดก่อนโจมตีครั้งต่อไป
        
        ResetAttack();
        canAttack = true; // ให้สามารถโจมตีได้อีกครั้ง
    }

    public void ResetAttack()
    {
        gatherInput.tryAttack = false;
        attackStarted = false;
        Debug.Log(attackStarted);
        Debug.Log("Resetting Attack");
    }

    public void ActivateAttack() 
    {
        attackCollider.enabled = true;    
    }

    public void ResetAttack2() 
    {
        attackCollider.enabled = false;
    }
}