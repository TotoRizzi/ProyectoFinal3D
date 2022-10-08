using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle : IState
{
    StateMachine _fsm;
    Enemy _myEnemy;
    StateName _stateToGo;
    public State_Idle(Enemy enemy, StateMachine fsm, StateName stateToGo)
    {
        _myEnemy = enemy;
        _fsm = fsm;
        _stateToGo = stateToGo;
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
        if (GameManager.instance.GetDirectionToPlayer(_myEnemy.transform).magnitude < _myEnemy.viewRange)
            _fsm.ChangeState(_stateToGo);
    }
}
