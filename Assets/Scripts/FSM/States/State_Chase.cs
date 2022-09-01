using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Chase : IState
{
    StateMachine _fsm;
    Enemy _myEnemy;
    public State_Chase(Enemy enemy, StateMachine fsm)
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
        _myEnemy.myMovement.Move();
    }

    public void OnUpdate()
    {
        _myEnemy.LookAtPlayer();
        
        if(GameManager.instance.GetDistanceToPlayer(_myEnemy.transform).magnitude < _myEnemy.attackRange)
            _fsm.ChangeState(StateName.Attack);
        else if (GameManager.instance.GetDistanceToPlayer(_myEnemy.transform).magnitude > _myEnemy.viewRange || _myEnemy.CanSeePlayer() == false)
            _fsm.ChangeState(StateName.Idle);
    }
}
