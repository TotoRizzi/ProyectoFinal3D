using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBlindWalk : IState
{
    StateMachine _fsm;
    Enemy _myEnemy;
    Transform _wallAndGroundCheckPosition;
    public StateBlindWalk(Enemy enemy, StateMachine fsm, Transform wallAndGroundCheckPosition)
    {
        _myEnemy = enemy;
        _fsm = fsm;
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
        _myEnemy.myMovement.Move();
    }

    public void OnUpdate()
    {
        if (Physics.Raycast(_wallAndGroundCheckPosition.position,_myEnemy.transform.right, 0.1f, GameManager.instance.WallLayer)
            ||
            !Physics.Raycast(_wallAndGroundCheckPosition.position, -_myEnemy.transform.up, 1, GameManager.instance.GroundLayer))
        {           
            _myEnemy.Flip();
        }
    }
}
