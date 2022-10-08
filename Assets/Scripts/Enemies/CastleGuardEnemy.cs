using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleGuardEnemy : Enemy
{
    public float attackRange;

    [Header("Mine")]
    [SerializeField] Transform _wallAndGroundCheckPosition;
    [SerializeField] float _walkingSpeed;
    [SerializeField] float _chasingSpeed;

    public override void Start()
    {
        base.Start();

        slowMovement = new RightMovement(this.transform, myRb, _walkingSpeed);
        fastMovement = new RightMovement(this.transform, myRb, _chasingSpeed);

        fsm.AddState(StateName.GroundWalk, new State_GroundWalk(this, fsm, _wallAndGroundCheckPosition));
        fsm.AddState(StateName.GroundChase, new State_GroundChase(this, fsm, _wallAndGroundCheckPosition));
        fsm.AddState(StateName.GroundAttack, new State_GroundAttack(this, fsm));
        fsm.ChangeState(StateName.GroundChase);
    }
    public override void Die()
    {
        isAlive = false;
        myRb.isKinematic = true;
        myCollider.enabled = false;

        myAnim.Play("Die");
    }
}
