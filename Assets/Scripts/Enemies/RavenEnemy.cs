using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenEnemy : SimpleRavenEnemy
{
    public float evadeTime;
    public float viewRange;
    public float evadeSpeed;

    protected override void Start()
    {
        _anim = GetComponent<Animator>();
        targetMovement = new DirectedMovement(transform, myRb, chargeSpeed, GameManager.instance.Player.transform);

        fsm.AddState(StateName.FlyingCharge, new State_FlyingCharge(this, _anim));
        fsm.AddState(StateName.FlyingEvade, new State_FlyingEvade(this, fsm, StateName.FlyingCharge));
        fsm.AddState(StateName.Idle, new State_Idle(this, fsm, StateName.FlyingEvade));

        fsm.ChangeState(StateName.Idle);
    }
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
