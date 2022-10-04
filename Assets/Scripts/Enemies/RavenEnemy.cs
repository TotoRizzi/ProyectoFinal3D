using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenEnemy : Enemy
{
    [SerializeField] float _chargeSpeed;
    public override void Start()
    {
        base.Start();

        fsm.AddState(StateName.FlyingCharge, new State_FlyingCharge());
        fsm.AddState(StateName.FlyingEvade, new State_FlyingEvade());
        fsm.AddState(StateName.Idle, new State_Idle(this, fsm, StateName.FlyingEvade));

        fsm.ChangeState(StateName.Idle);
    }
    public override void Die()
    {
        Debug.Log("Raven dead");
    }
}
