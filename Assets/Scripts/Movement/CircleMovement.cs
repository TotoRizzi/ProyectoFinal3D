using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : IMovement
{
    Rigidbody _myRb;
    Transform _myTransform;
    float _mySpeed;
    float _timeCounter;

    float _wideness = 2;

    public CircleMovement(Transform transform, Rigidbody rigidbody, float speed, float wideness)
    {
        _myRb = rigidbody;
        _myTransform = transform;
        _mySpeed = speed;
        _wideness = wideness;
    }

    public void Move()
    {
        _timeCounter += Time.deltaTime * _mySpeed;
        _myTransform.position = _myTransform.position + new Vector3(Mathf.Cos(_timeCounter) * _wideness, Mathf.Sin(_timeCounter) * _wideness, 0);
    }
}
