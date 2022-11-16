using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_FlyingCharge : IState
{
    Vector3 _dirToGo;
    Vector3 _velocity;

    SimpleRavenEnemy _myEnemy;
    public State_FlyingCharge(SimpleRavenEnemy enemy)
    {
        _myEnemy = enemy;
    }

    public void OnEnter()
    {
        _myEnemy.SetRavenIndicator();
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
        _dirToGo = GameManager.instance.GetDirectionToPlayer(_myEnemy.transform).normalized;

        _dirToGo *= _myEnemy.chargeSpeed;

        Vector3 steering = _dirToGo - _velocity;
        steering = Vector3.ClampMagnitude(steering, _myEnemy.maxMovingForce);
        ApplyForce(steering);
    }

    public void OnUpdate()
    {
        _myEnemy.LookAtPlayer();
        _myEnemy.transform.position += _velocity * Time.deltaTime;
    }

    private void ApplyForce(Vector3 force)
    {
        _velocity += force;
        _velocity = Vector3.ClampMagnitude(_velocity, _myEnemy.chargeSpeed);
    }

}
