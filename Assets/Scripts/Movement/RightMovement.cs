using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMovement : IMovement
{
    Rigidbody _myRb;
    Transform _myTransform;
    Transform _myModelTransform;
    float _mySpeed;

    public RightMovement(Transform transform, Transform modelTransform, Rigidbody rigidbody, float speed)
    {
        _myRb = rigidbody;
        _myTransform = transform;
        _mySpeed = speed;
        _myModelTransform = modelTransform;
    }
    public void Move()
    {
        _myRb.MovePosition(_myTransform.position + _myModelTransform.right * _mySpeed * Time.fixedDeltaTime);
    }
}
