using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public override void Start()
    {
        base.Start();
        myMovement = new DirectedMovement(this.transform, myRb, speed, GameManager.instance.Player.transform);
        fsm.AddState(StateName.Idle, new State_Idle(this, fsm));
        fsm.AddState(StateName.FlyingChase, new State_FlyingChase(this, fsm));
        fsm.AddState(StateName.FlyingAttack, new State_FlyingShootAttack(this, fsm));
        fsm.ChangeState(StateName.Idle);
    }
}
