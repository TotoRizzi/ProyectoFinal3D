using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Teleport : IState
{
    Necromancer _myEnemy;
    StateMachine _fsm;
    int _wayPointToTeleport;

    public State_Teleport(Necromancer myEnemy, StateMachine fsm)
    {
        _myEnemy = myEnemy;
        fsm = _fsm;
    }

    public void OnEnter()
    {
        _wayPointToTeleport = Random.Range(0, _myEnemy._myWaipoints.Length - 1);
    }

    public void OnExit()
    {

    }

    public void OnFixedUpdate()
    {

    }

    public void OnUpdate()
    {
        //Cuando la animacion de tepearse termine
        _myEnemy.transform.position = _myEnemy._myWaipoints[_wayPointToTeleport].transform.position;
        _fsm.ChangeState(StateName.InvokeRavens);
    }
}
