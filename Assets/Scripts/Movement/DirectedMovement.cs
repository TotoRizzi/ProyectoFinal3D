using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedMovement : IMovement
{
    Rigidbody _myRb;
    Transform _myTarget;
    Transform _myTransform;
    float _mySpeed;

    public DirectedMovement(Transform transform, Rigidbody rigidbody, float speed, Transform target)
    {
        _myRb = rigidbody;
        _myTarget = target;
        _myTransform = transform;
        _mySpeed = speed;
    }

    public void Move()
    {
        Vector3 dir = _myTarget.transform.position - _myTransform.position;
        dir.z = 0;

        _myRb.MovePosition(_myTransform.position + dir.normalized * _mySpeed * Time.fixedDeltaTime);
    }
}
