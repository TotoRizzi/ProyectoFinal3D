using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMovement : IMovement
{
    Rigidbody _myRb;
    Transform _myTransform;
    Vector3 _dir;
    float _mySpeed;

    public StraightMovement(Transform transform, float speed, Rigidbody rigidbody)
    {
        _myRb = rigidbody;
        _myTransform = transform;
        _mySpeed = speed;
    }
    public void Move()
    {
        _myRb.MovePosition(_myTransform.position + _myTransform.right * _mySpeed * Time.fixedDeltaTime);      
    }
}
