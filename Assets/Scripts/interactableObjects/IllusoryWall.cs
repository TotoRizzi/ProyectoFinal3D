using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusoryWall : MonoBehaviour, IDamageable
{
    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float dmg)
    {
        Die();
    }
}
