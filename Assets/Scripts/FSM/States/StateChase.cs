using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChase : IState
{
    StateMachine _fsm;
    Enemy _myEnemy;
    public StateChase(Enemy enemy, StateMachine fsm)
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

    public void OnUpdate()
    {
        _myEnemy.myMovement.Move();
        _myEnemy.LookAtPlayer();
        
        if (GameManager.instance.GetDistanceToPlayer(_myEnemy.transform).magnitude > _myEnemy.viewRange || _myEnemy.CanSeePlayer() == false)
            _fsm.ChangeState(StateName.Idle);
    }
}
