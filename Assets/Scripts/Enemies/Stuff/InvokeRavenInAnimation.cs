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
        Instantiate(_myEnemy.ravenPrefab, _myEnemy.shootingPoint.position, Quaternion.identity);      
    }
}
