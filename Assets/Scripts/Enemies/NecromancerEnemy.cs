using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerEnemy : Enemy
{
    [Header("TurnOff while Tping")]
    public GameObject[] myModels;

    [Header("To Tp")]
    public Transform[] myWaipoints;
    public float tpShaderOffSetY = .7f;
    public float idleTime;
    public Transform shootingPoint;
    public SimpleRavenEnemy ravenPrefab;
    AudioManager _audioManager;

    [Header("Animation Names")]
    public string invokeAnimationName;
    public string deathAnimationName;
    public string idleAnimationName;
    public string teleportAnimationName;

    protected override void Start()
    {
        fsm.AddState(StateName.Teleport, new State_Teleport(this, fsm));
        fsm.AddState(StateName.InvokeRavens, new State_InvokeRavens(this, fsm));
        fsm.AddState(StateName.StandingIdle, new State_StandingIdle(this, fsm));

        fsm.ChangeState(StateName.Teleport);
    }
    public override void Die()
    {
        base.Die();
        AudioManager.Instance.PlaySFX("NecroDeath");
        myAnim.Play(deathAnimationName);
    }
}
