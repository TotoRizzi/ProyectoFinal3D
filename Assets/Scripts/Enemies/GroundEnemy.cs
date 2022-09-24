using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy
{
    [SerializeField] Transform _wallAndGroundCheckPosition;
    [SerializeField] float _speed = 1;
    public override void Start()
    {
        base.Start();
        walkingMovement = new RightMovement(this.transform, myRb, _speed);
        fsm.AddState(StateName.BlindWalk, new State_BlindWalk(this, fsm, _wallAndGroundCheckPosition));
        fsm.ChangeState(StateName.BlindWalk);
    }
}
