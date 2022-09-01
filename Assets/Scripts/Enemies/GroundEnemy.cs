using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy
{
    Transform _wallAndGroundCheckPosition;
    public override void Start()
    {
        base.Start();
        _wallAndGroundCheckPosition = GameObject.Find("WallAndGroundCheckPosition").transform;
        myMovement = new RightMovement(this.transform, myRb, speed);
        fsm.AddState(StateName.BlindWalk, new State_BlindWalk(this, fsm, _wallAndGroundCheckPosition));
        fsm.ChangeState(StateName.BlindWalk);
    }
}
