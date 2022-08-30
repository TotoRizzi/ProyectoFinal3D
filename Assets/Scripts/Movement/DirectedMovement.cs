using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedMovement : IMovement
{
    Transform _myTarget;
    Transform _myTransform;
    float _mySpeed;

    public DirectedMovement(Transform transform, float speed, Transform target)
    {
        _myTarget = target;
        _myTransform = transform;
        _mySpeed = speed;
    }

    public void Move()
    {
        Vector3 dir = _myTarget.transform.position - _myTransform.position;

        _myTransform.position += dir.normalized * _mySpeed * Time.deltaTime;
    }
}
