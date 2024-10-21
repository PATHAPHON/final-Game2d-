using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRedDogAttack : EnemyAttack
{
    public float forceX;
    public float forceY;
    public float duration;
    private Rigidbody2D rb;

    PlayerMoveControls playerMoveControls;



    public override void SpecialAttack()
    {
        base.SpecialAttack();
        playerMoveControls = playerStats.GetComponentInParent<PlayerMoveControls>();
        // ตรวจสอบค่าของ forceX และ forceY
        Debug.Log("ForceX: " + forceX + ", ForceY: " + forceY);
        StartCoroutine(playerMoveControls.KnockBack(forceX, forceY, duration, transform));
    }
}
