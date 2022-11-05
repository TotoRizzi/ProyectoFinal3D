using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_StandingIdle : IState
{
    NecromancerEnemy _myEnemy;
    StateMachine _fsm;
    float currentIdleTime;

    public State_StandingIdle(NecromancerEnemy myEnemy, StateMachine fsm)
    {
        _myEnemy = myEnemy;
        _fsm = fsm;
    }
    public void OnEnter()
    {
        currentIdleTime = 0;
        _myEnemy.myAnim.Play(_myEnemy.idleAnimationName);
    }

    public void OnExit()
    {

    }

    public void OnFixedUpdate()
    {

    }

    public void OnUpdate()
    {
        currentIdleTime += Time.deltaTime;
        if (currentIdleTime > _myEnemy.idleTime) _fsm.ChangeState(StateName.Teleport);
    }
}
