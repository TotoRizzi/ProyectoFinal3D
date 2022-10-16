using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_StandingIdle : IState
{
    Necromancer _myEnemy;
    StateMachine _fsm;
    float currentIdleTime;

    public State_StandingIdle(Necromancer myEnemy, StateMachine fsm)
    {
        _myEnemy = myEnemy;
        _fsm = fsm;
    }
    public void OnEnter()
    {
        currentIdleTime = 0;
        //play a la animacion
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
        if (currentIdleTime > _myEnemy._idleTime) _fsm.ChangeState(StateName.Teleport);
    }
}
