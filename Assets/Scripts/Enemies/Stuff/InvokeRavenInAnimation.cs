using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeRavenInAnimation : MonoBehaviour
{
    NecromancerEnemy _myEnemy;

    private void Start()
    {
        _myEnemy = GetComponentInParent<NecromancerEnemy>();
    }

    public void InvokeRaven()
    {
        AudioManager.Instance.PlaySFX("NecromancerCast");
        FRY_NecromancerRaven.Instance.pool.GetObject().SetPosition(_myEnemy.shootingPoint.position);
        
    }
}
