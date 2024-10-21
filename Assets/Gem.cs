using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // ใช้สำหรับการเข้าถึง UI Text
public class Gem : MonoBehaviour
{
    public static int coinCount = 0;  // ตัวแปรเก็บจำนวนเหรียญ
    public Text coinText;      // อ้างอิง Text UI
    // Start is called before the first frame update
    void Start()
    {
        // ตั้งค่า Text เริ่มต้น
        coinText.text = " " + coinCount;
    }
   private void OnTriggerEnter2D(Collider2D collision)
   {
    
    if (collision.CompareTag("Player"))
       {
           
           coinCount++;
           Debug.Log(coinCount);
           // อัปเดตข้อความใน Text UI
           coinText.text = " " + coinCount;
           // ปิดการแสดงผลและตัวตรวจจับการชน
           GetComponent<SpriteRenderer>().enabled = false;
           GetComponent<Collider2D>().enabled = false;

           // เล่นเสียง
           AudioSource audioSource = GetComponent<AudioSource>();
           audioSource.Play();

           // เริ่มต้นคอร์รูทีนเพื่อรอจนกว่าเสียงจะเล่นจบ
           StartCoroutine(DestroyAfterSound(audioSource.clip.length));
       }
   }
   private IEnumerator DestroyAfterSound(float delay)
   {
       // รอจนกว่าเสียงจะเล่นจบ
       yield return new WaitForSeconds(delay);

       // ทำลายวัตถุ
       Destroy(gameObject);
   }


}