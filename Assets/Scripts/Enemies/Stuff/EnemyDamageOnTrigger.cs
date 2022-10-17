using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageOnTrigger : MonoBehaviour
{
    Enemy _myEnemy;
    [SerializeField] bool enemyScriptInThisObject;
    private void Start()
    {
        if (!enemyScriptInThisObject) _myEnemy = GetComponentInParent<Enemy>();
        else _myEnemy = GetComponent<Enemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        var dmg = other.GetComponent<IDamageable>();
        var player = other.GetComponent<Player>();
        if (dmg != null)
        {
            dmg.TakeDamage(_myEnemy.attackDmg);
            if (player)
                player.Knockback(_myEnemy.transform.position.x);
        }
    }
}
