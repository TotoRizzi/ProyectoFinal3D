using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_FlyingCharge : IState
{
    Vector3 _dirToGo;
    Vector3 _velocity;

    SimpleRavenEnemy _myEnemy;
    Animator _anim;
    public State_FlyingCharge(SimpleRavenEnemy enemy, Animator anim)
    {
        _myEnemy = enemy;
        _anim = anim;
    }

    public void OnEnter()
    {
        _anim.SetTrigger("Flap");
    }

    public void OnExit()
    {
        _anim.ResetTrigger("Flap");
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
