using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMovement : IMovement
{
    Rigidbody _myRb;
    Transform _myTransform;
    float _mySpeed;

    public RightMovement(Transform transform,Rigidbody rigidbody, float speed)
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
