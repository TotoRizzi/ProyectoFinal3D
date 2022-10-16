using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMovement : IMovement
{
    Transform[] _myWaypoints;
    Rigidbody _myRb;
    Transform _myTransform;
    float _mySpeed;

    int _index = -1;
    Vector3 _dir;

    public WayPointMovement(Transform transform, Rigidbody rigidbody, float speed, Transform[] myWaypoints)
    {
        _myRb = rigidbody;
        _myTransform = transform;
        _mySpeed = speed;
        _myWaypoints = myWaypoints;

        CalculateDir();
    }

    public void Move()
    {
        _myRb.MovePosition(_myTransform.position + _dir * _mySpeed * Time.fixedDeltaTime);

        if ((_myWaypoints[_index].transform.position - _myTransform.position).magnitude < .1f) CalculateDir();

    }

    void CalculateDir()
    {
        if (_index == _myWaypoints.Length - 1) _index = 0;
        else _index++;

        _dir = (_myWaypoints[_index].transform.position - _myTransform.position).normalized;
        _myTransform.right = _dir;
    }
}
