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
        _myEnemy.myAnim.Play(_myEnemy.invokeAnimationName);
    }

    public void OnExit()
    {

    }

    public void OnFixedUpdate()
    {

    }

    public void OnUpdate()
    {
        if (_myEnemy.myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            _fsm.ChangeState(StateName.StandingIdle);
    }
}
