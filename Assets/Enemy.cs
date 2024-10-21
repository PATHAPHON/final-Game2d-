using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 100f;

    protected Rigidbody2D rb;
    protected Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    } 

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // โค้ดจัดการเมื่อมอนสเตอร์ตาย เช่น ทำลายวัตถุหรือเล่นแอนิเมชันตาย
        Destroy(gameObject);
        Debug.Log("Gay");
    }
}
