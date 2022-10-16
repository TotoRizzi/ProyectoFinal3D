using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgOntrigger : MonoBehaviour
{
    [SerializeField] float dmg;
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        other.GetComponent<IDamageable>().TakeDamage(dmg);
        if (player) player.Knockback(transform.position.x);
    }
}
