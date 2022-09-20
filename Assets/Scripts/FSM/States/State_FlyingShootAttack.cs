using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_FlyingShootAttack : IState
{
    StateMachine _fsm;
    Enemy _myEnemy;

    public State_FlyingShootAttack(Enemy enemy, StateMachine fsm)
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
        _myEnemy.LookAtPlayer(true);

        if (!_myEnemy.CanSeePlayer()) _fsm.ChangeState(StateName.Idle);
        else if (GameManager.instance.GetDirectionToPlayer(_myEnemy.transform).magnitude > _myEnemy.attackRange)
            _fsm.ChangeState(StateName.FlyingChase);
        
    }
}
