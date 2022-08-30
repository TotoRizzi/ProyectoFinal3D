using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : IState
{
    StateMachine _fsm;
    Enemy _myEnemy;
    public StateIdle(Enemy enemy, StateMachine fsm)
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
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        if (GameManager.instance.GetDistanceToPlayer(_myEnemy.transform).magnitude < _myEnemy.viewRange && _myEnemy.CanSeePlayer())
            _fsm.ChangeState(StateName.Chase);
    }
}
