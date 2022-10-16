using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGroundEnemy : Enemy
{
    [SerializeField] float _mySpeed;
    public Transform[] _myWaypoints;
    public IMovement _myMovement;
    protected override void Start()
    {
        _myMovement = new WayPointMovement(transform, myRb, _mySpeed, _myWaypoints);

        fsm.AddState(StateName.WayPointWalk, new State_WayPointWalk(this));
        fsm.ChangeState(StateName.WayPointWalk);
    }
}
