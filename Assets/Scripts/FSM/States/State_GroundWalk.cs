using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_GroundWalk : IState
{
    StateMachine _fsm;
    Enemy _myEnemy;
    Transform _wallAndGroundCheckPosition;
    public State_GroundWalk(Enemy enemy, StateMachine fsm, Transform wallAndGroundCheckPosition)
    {
        _myEnemy = enemy;
        _fsm = fsm;
        _wallAndGroundCheckPosition = wallAndGroundCheckPosition;
    }

    public void OnEnter()
    {
        _myEnemy.myAnim.Play("Walking");
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
        _myEnemy.walkingMovement.Move();
    }

    public void OnUpdate()
    {
        if (Physics.Raycast(_wallAndGroundCheckPosition.position, _myEnemy.transform.right, 0.1f, GameManager.instance.WallLayer)
            ||
            !Physics.Raycast(_myEnemy.transform.position, -_myEnemy.transform.up, 2.0f, GameManager.instance.GroundLayer)
            ||
            Physics.Raycast(_wallAndGroundCheckPosition.position, _myEnemy.transform.right, 0.1f, GameManager.instance.GroundLayer))
        {
            _myEnemy.Flip();
        }
        Debug.Log("Walking");

        if (GameManager.instance.GetDirectionToPlayer(_myEnemy.transform).magnitude < _myEnemy.viewRange && _myEnemy.CanSeePlayer())
            _fsm.ChangeState(StateName.GroundChase);
    }
}
