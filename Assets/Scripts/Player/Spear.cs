using System;
using UnityEngine;
public class Spear : MonoBehaviour
{
    [SerializeField] protected float _damage;
    protected virtual void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<Enemy>();

        if (damageable != null)
            damageable.TakeDamage(_damage);
    }
}
