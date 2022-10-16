using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_WayPointWalk : IState    
{
    SimpleGroundEnemy _myEnemy;
    public State_WayPointWalk(SimpleGroundEnemy myEnemy)
    {
        _myEnemy = myEnemy;
    }
    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void OnFixedUpdate()
    {
        _myEnemy._myMovement.Move();
    }

    public void OnUpdate()
    {

    }
}
