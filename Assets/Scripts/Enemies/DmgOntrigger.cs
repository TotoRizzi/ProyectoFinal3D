using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgOntrigger : MonoBehaviour
{
    [SerializeField] float dmg;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        other.GetComponent<IDamageable>().TakeDamage(dmg);
    }
}
