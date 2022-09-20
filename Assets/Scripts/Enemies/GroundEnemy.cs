using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy
{
    [SerializeField] Transform _wallAndGroundCheckPosition;
    public override void Start()
    {
        base.Start();
        myMovement = new RightMovement(this.transform, myRb, speed);
        fsm.AddState(StateName.BlindWalk, new State_BlindWalk(this, fsm, _wallAndGroundCheckPosition));
        fsm.ChangeState(StateName.BlindWalk);
    }
}
