using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_GroundChase : IState
{
    StateMachine _fsm;
    Enemy _myEnemy;
    Transform _wallAndGroundCheckPosition;

    float _sphereCastLengh = 10f;
    float _sphereCastRadius = 1.5f;
    public State_GroundChase(Enemy enemy, StateMachine fsm, Transform wallAndGroundCheckPosition)
    {
        _myEnemy = enemy;
        _fsm = fsm;
        _wallAndGroundCheckPosition = wallAndGroundCheckPosition;
    }

    public void OnEnter()
    {
        _myEnemy.myAnim.Play("Chasing");
    }

    public void OnExit()
    {

    }

    public void OnFixedUpdate()
    {
        //Si enfrente no tiene ninguna pared o piso, caminar
        if(Physics.Raycast(_wallAndGroundCheckPosition.position, -_myEnemy.transform.up, 2.0f, GameManager.instance.GroundLayer)) 
            _myEnemy.chasingMovement.Move();
    }

    public void OnUpdate()
    {
        _myEnemy.LookAtPlayer(false);
        //Debug.Log("Chasing");

        if (GameManager.instance.GetDirectionToPlayer(_myEnemy.transform).magnitude < _myEnemy.attackRange || Physics.SphereCast(_myEnemy.transform.position, _sphereCastRadius, Vector3.up, out RaycastHit hitIndo, _sphereCastLengh, GameManager.instance.PlayerLayer))
            _fsm.ChangeState(StateName.GroundAttack);
        else if (GameManager.instance.GetDirectionToPlayer(_myEnemy.transform).magnitude > _myEnemy.viewRange || _myEnemy.CanSeePlayer() == false)
            _fsm.ChangeState(StateName.GroundWalk);
    }
}
