using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float health;

    private bool canTakeDamage = true;

    private Animator anim;

    private bool isDead;
    public GameMangerScript gameManger;

    [SerializeField] private Image _healthBarFill;


    void Start()
    {
        anim = GetComponentInParent<Animator>();
        health = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        if(!canTakeDamage)
        {
            return;
        }

        health -= damage;
        anim.SetBool("Damage", true);
        if(health <= 0 && !isDead)
        {

            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponentInParent<GatherInput1>().DisableControls();
            gameManger.gameOver();
            isDead = true;
            Debug.Log("Player is dead");
        }
        StartCoroutine(DamagePrevention());
        UpdateHealthBar();
    }
    private IEnumerator DamagePrevention() 
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.15f);
        if (health > 0 && !isDead)
        {
            canTakeDamage = true;
            anim.SetBool("Damage", false);
        }
        else 
        {
            anim.SetBool("Death", true);
        }
    }

    private void UpdateHealthBar() {

       StartCoroutine(SmoothHealthBarTransition());
    }


    private IEnumerator SmoothHealthBarTransition() {
       float elapsedTime = 0f;
       float currentFill = _healthBarFill.fillAmount;
       float targetFill = health / maxHealth;
    
       while (elapsedTime < 0.5f) {
           _healthBarFill.fillAmount = Mathf.Lerp(currentFill, targetFill, (elapsedTime / 0.5f));
           elapsedTime += Time.deltaTime;
           yield return null;
       }
       _healthBarFill.fillAmount = targetFill;
    }
}
    

