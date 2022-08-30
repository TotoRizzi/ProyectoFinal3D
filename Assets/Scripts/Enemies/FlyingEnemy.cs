using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    private void Start()
    {
        myMovement = new DirectedMovement(this.transform, speed, GameManager.instance.Player.transform);
        fsm.AddState(StateName.Idle, new StateIdle(this, fsm));
        fsm.AddState(StateName.Chase, new StateChase(this, fsm));
        fsm.ChangeState(StateName.Idle);
    }
}
