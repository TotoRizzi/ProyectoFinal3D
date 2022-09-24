using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageOnTrigger : MonoBehaviour
{
    Enemy _myEnemy;
    private void Start()
    {
        _myEnemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IDamageable>().TakeDamage(_myEnemy.attackDmg);
    }
}
