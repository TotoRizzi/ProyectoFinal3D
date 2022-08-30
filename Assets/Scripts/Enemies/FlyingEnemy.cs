using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public override void Start()
    {
        base.Start();
        myMovement = new DirectedMovement(this.transform, myRb, speed, GameManager.instance.Player.transform);
        fsm.AddState(StateName.Idle, new StateIdle(this, fsm));
        fsm.AddState(StateName.Chase, new StateChase(this, fsm));
        fsm.ChangeState(StateName.Idle);
    }
}
