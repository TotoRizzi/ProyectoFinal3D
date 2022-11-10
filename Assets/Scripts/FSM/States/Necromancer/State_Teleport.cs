using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Teleport : IState
{
    NecromancerEnemy _myEnemy;
    StateMachine _fsm;
    int _wayPointToTeleport;
    int _prebWaypoint;

    float _tpTimer = 2;
    float _currentTpTimer = 0;
    public State_Teleport(NecromancerEnemy myEnemy, StateMachine fsm)
    {
        _myEnemy = myEnemy;
        _fsm = fsm;
    }

    public void OnEnter()
    {
        _currentTpTimer = 0;

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

        _currentTpTimer += Time.deltaTime;

        FRY_NecromancerDisappearParticle.Instance.pool.GetObject().SetPosition(_myEnemy.transform.position);
        FRY_NecromancerTpParicle.Instance.pool.GetObject().SetPosition(_myEnemy.myWaipoints[_wayPointToTeleport].transform.position + _myEnemy.transform.up * _myEnemy.tpShaderOffSetY);
        foreach (var item in _myEnemy.myModels)
        {
            item.SetActive(false);
        }
        if(_currentTpTimer > _tpTimer)
        {
            _myEnemy.transform.position = _myEnemy.myWaipoints[_wayPointToTeleport].transform.position;
            foreach (var item in _myEnemy.myModels)
            {
                item.SetActive(true);
            }
            _fsm.ChangeState(StateName.InvokeRavens);
        }
    }
}
