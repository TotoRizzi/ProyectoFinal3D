using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangeeEnemy : Enemy
{
    public float speed = .1f;
    public float attackSpeed = 5;

    public override void LookAtPlayer()
    {
        if (GameManager.instance.Player.transform.position.x > transform.position.x)
        {
            isFacingRight = false;
            transform.rotation = Quaternion.Euler(0, 55, 0);
        }
        else
        {
            isFacingRight = true;
            transform.rotation = Quaternion.Euler(0, 110, 0);
        }
    }
}
