using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_GroundChase : IState
{
    StateMachine _fsm;
    Enemy _myEnemy;
    Transform _target;
    Transform _wallAndGroundCheckPosition;
    public State_GroundChase(Enemy enemy, StateMachine fsm, Transform wallAndGroundCheckPosition, Transform target)
    {
        _myEnemy = enemy;
        _fsm = fsm;
        _target = target;
        _wallAndGroundCheckPosition = wallAndGroundCheckPosition;
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
        Debug.Log("CHASING PLAYER");
        _myEnemy.LookAtPlayer(false);

        //Si enfrente no tiene ninguna pared o piso, caminar
        if(Physics.Raycast(_wallAndGroundCheckPosition.position, -_myEnemy.transform.up, 2.0f, GameManager.instance.GroundLayer)) 
            _myEnemy.myMovement.Move();

       /* if (GameManager.instance.GetDirectionToPlayer(_myEnemy.transform).magnitude < _myEnemy.attackRange)
            _fsm.ChangeState(StateName.GroundAttack);
        else if (GameManager.instance.GetDirectionToPlayer(_myEnemy.transform).magnitude > _myEnemy.viewRange || _myEnemy.CanSeePlayer() == false)
            _fsm.ChangeState(StateName.Idle);*/
    }
}
