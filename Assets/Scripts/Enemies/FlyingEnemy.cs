using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    [SerializeField] float _speed = 1;

    public override void Start()
    {
        base.Start();
        walkingMovement = new DirectedMovement(this.transform, myRb, _speed, GameManager.instance.Player.transform);
        fsm.AddState(StateName.Idle, new State_Idle(this, fsm, StateName.FlyingCharge));
        fsm.AddState(StateName.FlyingChase, new State_FlyingChase(this, fsm));
        fsm.AddState(StateName.FlyingAttack, new State_FlyingShootAttack(this, fsm));
        fsm.ChangeState(StateName.Idle);
    }
}
