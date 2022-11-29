using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeRavenInAnimation : MonoBehaviour
{
    NecromancerEnemy _myEnemy;
    AudioManager _audioManager;

    private void Start()
    {
        _myEnemy = GetComponentInParent<NecromancerEnemy>();
    }

    public void InvokeRaven()
    {
        _audioManager.PlaySFX("NecromancerCast");
        FRY_NecromancerRaven.Instance.pool.GetObject().SetPosition(_myEnemy.shootingPoint.position);
        
    }
}
