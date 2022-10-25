using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_FlyingEvade : IState
{
    Vector3 dirToGo;

    RavenEnemy _myEnemy;
    StateMachine _fsm;
    StateName _stateToGo;

    float _currentEvadeTime;
    public State_FlyingEvade(RavenEnemy enemy, StateMachine fsm, StateName stateToGo)
    {
        _myEnemy = enemy;
        _fsm = fsm;
        _stateToGo = stateToGo;
    }
    public void OnEnter()
    {
        dirToGo = new Vector3(Random.Range(-10,11), Random.Range(0,11), _myEnemy.transform.position.z).normalized;
    }

    public void OnExit()
    {
        _currentEvadeTime = 0;
    }

    public void OnFixedUpdate()
    {
        //.dirMovement.Move(dirToGo, _myEnemy.evadeSpeed);
    }

    public void OnUpdate()
    {
        _myEnemy.transform.position += dirToGo * _myEnemy.evadeSpeed * Time.deltaTime;
        _currentEvadeTime += Time.deltaTime;

        if (_currentEvadeTime > _myEnemy.evadeTime)
            _fsm.ChangeState(_stateToGo);
    }
}
