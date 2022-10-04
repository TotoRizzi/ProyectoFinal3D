using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _maxLife;
    [SerializeField] protected float _currentLife;
    protected virtual void Start()
    {
        _currentLife = _maxLife;
    }
    public virtual void TakeDamage(float dmg)
    {
        _currentLife -= dmg;
        if (_currentLife <= 0)
            Die();
    }
    public virtual void Die()
    {
        Debug.Log(name + "ha muerto");
    }
}
