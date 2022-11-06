using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_FollowPlayer : IState
{
    BangeeEnemy _myEnemy;
    StateMachine _fsm;

    public State_FollowPlayer(BangeeEnemy myEnemy, StateMachine fsm)
    {
        _myEnemy = myEnemy;
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
        _myEnemy.directionalMovement.Move();
    }

    public void OnUpdate()
    { 
        Debug.Log("Following Player");
       
        if (GameManager.instance.GetDirectionToPlayer(_myEnemy.transform).magnitude <= _myEnemy.circleRange) _fsm.ChangeState(StateName.CirclePlayer);
    }
}
