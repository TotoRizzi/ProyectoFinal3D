using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_InvokeRavens : IState
{ 
    Necromancer _myEnemy;
    StateMachine _fsm;

    public State_InvokeRavens(Necromancer myEnemy, StateMachine fsm)
    {
        _myEnemy = myEnemy;
        _fsm = fsm;
    }
    public void OnEnter()
    {
        //Play la animacion y que dispare por ahi
    }

    public void OnExit()
    {

    }

    public void OnFixedUpdate()
    {

    }

    public void OnUpdate()
    {
        //Cuando se termina la animacion, cambie al state Idle
    }
}
