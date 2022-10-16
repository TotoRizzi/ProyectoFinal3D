using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : Enemy
{
    public float _idleTime;
    public Transform[] _myWaipoints;

    protected override void Start()
    {
        fsm.AddState(StateName.Teleport, new State_Teleport(this, fsm));
        fsm.AddState(StateName.InvokeRavens, new State_InvokeRavens(this, fsm));
        fsm.AddState(StateName.StandingIdle, new State_StandingIdle(this, fsm));
        fsm.ChangeState(StateName.Teleport);
    }
}
