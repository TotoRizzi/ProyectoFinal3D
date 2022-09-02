using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack : IState
{
    StateMachine _fsm;
    Enemy _myEnemy;

    public State_Attack(Enemy enemy, StateMachine fsm)
    {
        _myEnemy = enemy;
        _fsm = fsm;
    }

    public void OnEnter()
    {
        Debug.Log("Attack OnEnter");
    }

    public void OnExit()
    {
        Debug.Log("Attack OnExit");
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        _myEnemy.LookAtPlayer();

        if (!_myEnemy.CanSeePlayer()) _fsm.ChangeState(StateName.Idle);
        else if (GameManager.instance.GetDistanceToPlayer(_myEnemy.transform).magnitude > _myEnemy.attackRange)
            _fsm.ChangeState(StateName.Chase);
        
    }
}
