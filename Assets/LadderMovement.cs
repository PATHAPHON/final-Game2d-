using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    private float speed = 8f;
    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        // ตั้งค่า gravityScale เริ่มต้น
        rb.gravityScale = 4f; // ปรับให้เหมาะสมกับการเคลื่อนไหวทั่วไป
    }

    void Update()
    {
        // ตรวจสอบว่าผู้เล่นอยู่บนบันไดและกด Spacebar เพื่อเริ่มปีน
        if (isLadder && Input.GetKeyDown(KeyCode.Space))
        {
            isClimbing = true;
        }

        // ปรับค่าการควบคุมในแนวตั้งสำหรับการปีน
        if (isClimbing)
        {
            vertical = Input.GetAxis("Vertical"); // รับค่าการควบคุมในแนวตั้ง (W/S หรือ ปุ่มลูกศรขึ้น/ลง)
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f; // ปิดแรงโน้มถ่วงระหว่างการปีน
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed); // ตั้งค่าความเร็วในการปีน

            // ให้ความเร็วในการปีนเพิ่มขึ้นเมื่อกด Space ค้าง
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, speed); // ความเร็วในการปีน
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector2(rb.velocity.x, -speed); // ความเร็วในการลง
            }
        }
        else
        {
            // ไม่มีการเปลี่ยนแปลง gravityScale ที่นี่ เพื่อให้มันคงอยู่ตามค่าเริ่มต้น
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            Debug.Log(isLadder);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false; // หยุดการปีนเมื่อออกจากบันได
            rb.gravityScale = 4f; // ตั้งค่า gravityScale กลับเป็นค่าเริ่มต้นเมื่อออกจากบันได
            Debug.Log(isClimbing);
            Debug.Log(isLadder);
        }
    }
}
