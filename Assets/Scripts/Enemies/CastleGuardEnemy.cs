using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleGuardEnemy : Enemy
{
    [SerializeField] Transform _wallAndGroundCheckPosition;
    public override void Start()
    {
        base.Start();
        myMovement = new RightMovement(this.transform, myRb, speed);
        fsm.AddState(StateName.GroundWalk, new State_GroundWalk());
        fsm.AddState(StateName.GroundChase, new State_GroundChase(this, fsm, _wallAndGroundCheckPosition, GameManager.instance.Player.transform));
        fsm.ChangeState(StateName.GroundChase);
    }
}
