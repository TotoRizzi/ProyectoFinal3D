using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangeeChildEnemy : MonoBehaviour
{
    float _timeCounter;
    [SerializeField] float _speed = .5f;
    [SerializeField] float _wideness = 2;
    
    [SerializeField] Transform bangeeParent;
    private void Update()
    {
        _timeCounter += Time.deltaTime * _speed;
        transform.position = bangeeParent.position + new Vector3(Mathf.Cos(_timeCounter) * _wideness, Mathf.Sin(_timeCounter) * _wideness, 0);
    }
}
