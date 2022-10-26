using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Teleport : IState
{
    Necromancer _myEnemy;
    StateMachine _fsm;
    int _wayPointToTeleport;
    int _prebWaypoint;

    public State_Teleport(Necromancer myEnemy, StateMachine fsm)
    {
        _myEnemy = myEnemy;
        _fsm = fsm;
    }

    public void OnEnter()
    {
        int newWaypoint = Random.Range(0, _myEnemy.myWaipoints.Length - 1);

        if(newWaypoint == _prebWaypoint)
        {
            while (newWaypoint == _prebWaypoint)
                newWaypoint = Random.Range(0, _myEnemy.myWaipoints.Length - 1);
        }

        _wayPointToTeleport = newWaypoint;
        _prebWaypoint = _wayPointToTeleport;
        _myEnemy.myAnim.Play(_myEnemy.teleportAnimationName);
    }

    public void OnExit()
    {

    }

    public void OnFixedUpdate()
    {

    }

    public void OnUpdate()
    {
        if (_myEnemy.myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1) return;

        FRY_NecromancerDisappearParticle.Instance.pool.GetObject().SetPosition(_myEnemy.transform.position);
        _myEnemy.transform.position = _myEnemy.myWaipoints[_wayPointToTeleport].transform.position;
        _fsm.ChangeState(StateName.InvokeRavens);
    }
}
