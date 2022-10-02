using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_GroundAttack : IState
{
    Enemy _myEnemy;
    StateMachine _fsm;
    public State_GroundAttack(Enemy myEnemy, StateMachine fsm)
    {
        _myEnemy = myEnemy;
        _fsm = fsm;
    }
    public void OnEnter()
    {
        _myEnemy.myAnim.Play("Attack " + Random.Range(1, 3).ToString());
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        
        //Debug.Log("Attack");
        if (_myEnemy.myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            _fsm.ChangeState(StateName.GroundChase);
        
    }
}
