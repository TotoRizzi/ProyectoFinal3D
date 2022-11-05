using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_FollowPlayer : IState
{
    BangeeEnemy _myEnemy;

    public State_FollowPlayer(BangeeEnemy myEnemy)
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

    }

    public void OnUpdate()
    {
        _myEnemy.transform.position += (GameManager.instance.GetDirectionToPlayer(_myEnemy.transform) * _myEnemy.speed * Time.deltaTime);
    }
}
