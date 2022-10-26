using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeRavenInAnimation : MonoBehaviour
{
    Necromancer _myEnemy;

    private void Start()
    {
        _myEnemy = GetComponentInParent<Necromancer>();
    }

    public void InvokeRaven()
    {
        FRY_NecromancerRaven.Instance.pool.GetObject().SetPosition(_myEnemy.shootingPoint.position);
    }
}
