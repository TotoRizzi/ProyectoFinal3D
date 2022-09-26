using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour, IDamageable
{
    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float dmg)
    {
        Debug.Log("Dummy Hitted");
    }
}
