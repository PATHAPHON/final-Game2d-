using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    private PlayerMoveControls playerMoveControlss;

    public float speed = 5f;
    private GatherInput1 gatherInput;
    private Rigidbody2D rigidbody2D;

    private int direction = 1; // ไปทางขวา

    private Animator animator;

    public float jumpForce;
    public float rayLength;
    public LayerMask groundLayer;
    public Transform leftPoint;
    private bool grounded = false;

    // ตัวแปรการดาช
    public float dashSpeed = 20f; // ความเร็วขณะดาช
    public float dashDuration = 1f; // ระยะเวลาที่ดาช
    public float dashCooldown = 1f; // เวลาคูลดาวน์ระหว่างการดาช
    private bool isDashing = false; // ตัวละครกำลังดาชอยู่หรือไม่
    private float dashTime; // เวลาที่เหลือสำหรับการดาชในปัจจุบัน
    private float nextDashTime = 0f; // เวลาเมื่อสามารถดาชได้อีกครั้ง

    public bool attackStarted = false;

    private bool knockBack = false;    

    void Start()
    {
        gatherInput = GetComponent<GatherInput1>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ตรวจสอบการดาช
        if (gatherInput.dashInput && Time.time >= nextDashTime)
        {
            StartDash();
        }

        if (isDashing)
        {
            // ลดเวลาการดาช
            dashTime -= Time.deltaTime;

            // ตรวจสอบว่าการดาชเสร็จสิ้นหรือไม่
            if (dashTime <= 0)
            {
                isDashing = false;
            }
        }
        
        // ดำเนินการเคลื่อนที่และตั้งค่าค่าของอนิเมเตอร์
        if (!isDashing)
        {
            rigidbody2D.velocity = new Vector2(speed * gatherInput.valueX, rigidbody2D.velocity.y);
        }

        SetAnimatorValues();
    }

    void FixedUpdate()
    {
        CheckStatus();
        if (!isDashing)
        {
            Move();
            JumpPlayer();
        }
        if(knockBack) return;
    }

    private void Move() 
    {
        Flip();
        if (gatherInput.valueX != 0)
        {
           rigidbody2D.velocity = new Vector2(speed * gatherInput.valueX, rigidbody2D.velocity.y);
        }
    }

    private void Flip() 
    { 
        if (gatherInput.valueX * direction < 0) 
        {
           transform.Rotate(0, 180, 0);
           rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
           direction *= -1;
        }
    }

    private void JumpPlayer() 
    {
        if (gatherInput.jumpInput && grounded)
        {
            rigidbody2D.velocity = new Vector2(gatherInput.valueX * speed, jumpForce);
        }
        gatherInput.jumpInput = false;
    } 

    private void SetAnimatorValues() 
    {
        animator.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x));
        animator.SetFloat("vSpeed", rigidbody2D.velocity.y);
        animator.SetBool("Grounded", grounded);
    }

    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        grounded = leftCheckHit;
    }

    private void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        nextDashTime = Time.time + dashCooldown; // ตั้งเวลาในการดาชถัดไป
        float originalGravity = rigidbody2D.gravityScale;
        rigidbody2D.gravityScale= 0f;
        rigidbody2D.velocity = new Vector2(direction * dashSpeed , 0f); 
        StartCoroutine(ResetGravity(originalGravity)); // เรียกใช้ Coroutine สำหรับดีเลย์การคืนค่า gravity
        gatherInput.dashInput = false; // รีเซ็ตการป้อนข้อมูลการดาช
        animator.SetTrigger("Dash");
    }

    private IEnumerator ResetGravity(float originalGravity)
    {
       yield return new WaitForSeconds(0.2f); // ตั้งค่าดีเลย์ 0.2 วินาทีหรือปรับตามความต้องการ
       rigidbody2D.gravityScale = originalGravity; // คืนค่า gravityScale กลับเป็นค่าปกติ
    }

    public IEnumerator KnockBack(float forceX, float forceY, float duration, Transform otherObject)
    {
       int knockBackDirection;
       if (transform.position.x < otherObject.position.x)
       {
           knockBackDirection = -1; // ถ้าตำแหน่งนี้อยู่ทางซ้าย
       }
       else
       {
           knockBackDirection = 1; // ถ้าตำแหน่งนี้อยู่ทางขวา
       }

       knockBack = true;
       rigidbody2D.velocity = Vector2.zero; // ตั้งความเร็วเริ่มต้นเป็นศูนย์
       Vector2 theForce = new Vector2(forceX * knockBackDirection, forceY);
    
       Debug.Log("Applying knockback force: " + theForce); // Debug เพื่อตรวจสอบแรงที่ใช้
       rigidbody2D.AddForce(theForce, ForceMode2D.Impulse); // ใช้แรง

       yield return new WaitForSeconds(duration);
       knockBack = false;
       rigidbody2D.velocity = Vector2.zero; // ตั้งความเร็วหลังจากหมดเวลา
    }



}
