using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleGuardEnemy : Enemy
{
    [Header("Ranges Movement")]
    public float viewRange;
    public float attackRange;

    public IMovement slowMovement;
    public IMovement fastMovement;

    [Header("Mine")]
    [SerializeField] Transform _wallAndGroundCheckPosition;
    [SerializeField] float _walkingSpeed;
    [SerializeField] float _chasingSpeed;

    protected override void Start()
    {
        base.Start();

        slowMovement = new StraightMovement(this.transform, _walkingSpeed, myRb);
        fastMovement = new StraightMovement(this.transform, _chasingSpeed, myRb);

        fsm.AddState(StateName.GroundWalk, new State_GroundWalk(this, fsm, _wallAndGroundCheckPosition));
        fsm.AddState(StateName.GroundChase, new State_GroundChase(this, fsm, _wallAndGroundCheckPosition));
        fsm.AddState(StateName.GroundAttack, new State_GroundAttack(this, fsm));
        fsm.ChangeState(StateName.GroundWalk);
    }
    public override void Die()
    {
        base.Die();

        myAnim.Play("Die");
    }
}
