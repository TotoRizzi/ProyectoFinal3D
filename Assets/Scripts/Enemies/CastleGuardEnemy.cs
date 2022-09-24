using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleGuardEnemy : Enemy
{
    [SerializeField] Transform _wallAndGroundCheckPosition;
    [SerializeField] float _walkingSpeed;
    [SerializeField] float _chasingSpeed;

    public override void Start()
    {
        base.Start();

        walkingMovement = new RightMovement(this.transform, myRb, _walkingSpeed);
        chasingMovement = new RightMovement(this.transform, myRb, _chasingSpeed);

        fsm.AddState(StateName.GroundWalk, new State_GroundWalk(this, fsm, _wallAndGroundCheckPosition));
        fsm.AddState(StateName.GroundChase, new State_GroundChase(this, fsm, _wallAndGroundCheckPosition, GameManager.instance.Player.transform));
        fsm.AddState(StateName.GroundAttack, new State_GroundAttack(this, fsm));
        fsm.ChangeState(StateName.GroundChase);
    }
}
