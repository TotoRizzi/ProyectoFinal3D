using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_FlyingChase : IState
{
    StateMachine _fsm;
    Enemy _myEnemy;
    public State_FlyingChase(Enemy enemy, StateMachine fsm)
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
        _myEnemy.walkingMovement.Move();
    }

    public void OnUpdate()
    {
        _myEnemy.LookAtPlayer(true);
        
        if(GameManager.instance.GetDirectionToPlayer(_myEnemy.transform).magnitude < _myEnemy.attackRange)
            _fsm.ChangeState(StateName.FlyingAttack);
        else if (GameManager.instance.GetDirectionToPlayer(_myEnemy.transform).magnitude > _myEnemy.viewRange || _myEnemy.CanSeePlayer() == false)
            _fsm.ChangeState(StateName.Idle);
    }
}
