using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMovement : IMovement
{
    Rigidbody _myRb;
    Transform _myTransform;
    Vector3 _dir;
    float _mySpeed;
    bool _physics;

    public StraightMovement(Transform transform, float speed, Vector3 dir, bool physics, Rigidbody rigidbody = null)
    {
        _myRb = rigidbody;
        _myTransform = transform;
        _mySpeed = speed;
        _dir = dir.normalized;
        _physics = physics;
    }
    public void Move()
    {
        if (_physics) _myRb.MovePosition(_myTransform.position + _dir * _mySpeed * Time.fixedDeltaTime);
        else _myTransform.position += _dir * Time.deltaTime * _mySpeed;
    }
}
