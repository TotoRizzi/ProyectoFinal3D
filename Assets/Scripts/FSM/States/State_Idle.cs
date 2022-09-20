using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle : IState
{
    StateMachine _fsm;
    Enemy _myEnemy;
    public State_Idle(Enemy enemy, StateMachine fsm)
    {
        _myEnemy = enemy;
        _fsm = fsm;
    }
    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        if (GameManager.instance.GetDirectionToPlayer(_myEnemy.transform).magnitude < _myEnemy.viewRange && _myEnemy.CanSeePlayer())
            _fsm.ChangeState(StateName.FlyingChase);
    }
}
