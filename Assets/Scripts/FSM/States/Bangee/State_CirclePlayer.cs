using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_CirclePlayer : IState
{
    BangeeEnemy _myEnemy;
    StateMachine _fsm;

    public State_CirclePlayer(BangeeEnemy myEnemy, StateMachine fsm)
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
    }

    public void OnUpdate()
    {
        Debug.Log("circleing Player");
      
        _myEnemy.circleMovement.Move();

        if (GameManager.instance.GetDirectionToPlayer(_myEnemy.transform).magnitude >= _myEnemy.stopCircleRange) _fsm.ChangeState(StateName.FollowPlayer);
    }
}
