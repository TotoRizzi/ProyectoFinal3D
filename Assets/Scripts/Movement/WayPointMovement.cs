using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMovement : IMovement
{
    Transform[] _myWaypoints;
    Rigidbody _myRb;
    Transform _myTransform;
    Transform _myGroundCheck;
    float _mySpeed;
    bool _adjustToGround;

    int _index = -1;
    Vector3 _dir;

    public WayPointMovement(Transform transform, Rigidbody rigidbody, float speed, Transform[] myWaypoints ,Transform groundCheck = null, bool adjustToGround = false)
    {
        _myRb = rigidbody;
        _myTransform = transform;
        _mySpeed = speed;
        _myWaypoints = myWaypoints;
        _myGroundCheck = groundCheck;
        _adjustToGround = adjustToGround;
        CalculateDir();
    }

    public void Move()
    {
        //_myRb.MovePosition(_myTransform.position + _dir * _mySpeed * Time.fixedDeltaTime);
        _myTransform.position = Vector3.MoveTowards(_myTransform.position, _myWaypoints[_index].position, _mySpeed * Time.deltaTime);
        if ((_myWaypoints[_index].transform.position - _myTransform.position).magnitude < .1f) CalculateDir();

    }

    void CalculateDir()
    {
        if (_index == _myWaypoints.Length - 1) _index = 0;
        else _index++;

        _dir = (_myWaypoints[_index].transform.position - _myTransform.position).normalized;
        
        if (!_adjustToGround) return;

        _myTransform.right = _dir;
        if (Physics.Raycast(_myGroundCheck.position, _myGroundCheck.up, 0.2f, GameManager.instance.GroundLayer))
            _myTransform.localScale = new Vector3(_myTransform.localScale.x, _myTransform.localScale.y * -1 , _myTransform.localScale.z);
    }
}
