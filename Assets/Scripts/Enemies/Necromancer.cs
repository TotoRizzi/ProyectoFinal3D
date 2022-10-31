using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : Enemy
{
    public float idleTime;
    public Transform[] myWaipoints;
    public Transform shootingPoint;
    public SimpleRaven ravenPrefab;

    public string invokeAnimationName;
    public string deathAnimationName;
    public string idleAnimationName;
    public string teleportAnimationName;

    protected override void Start()
    {
        fsm.AddState(StateName.Teleport, new State_Teleport(this, fsm));
        fsm.AddState(StateName.InvokeRavens, new State_InvokeRavens(this, fsm));
        fsm.AddState(StateName.StandingIdle, new State_StandingIdle(this, fsm));

        fsm.ChangeState(StateName.InvokeRavens);
    }
    public override void Die()
    {
        base.Die();
        myAnim.Play(deathAnimationName);
    }
}
