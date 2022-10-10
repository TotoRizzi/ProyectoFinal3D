using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenEnemy : Enemy
{
    [SerializeField] float _dmg;
    public float evadeTime;

    public IMovement targetMovement;

    public float evadeSpeed;
    public float chargeSpeed;
    public float maxMovingForce;


    public override void Start()
    {
        base.Start();
        
        targetMovement = new DirectedMovement(transform, myRb, chargeSpeed, GameManager.instance.Player.transform);

        fsm.AddState(StateName.FlyingCharge, new State_FlyingCharge(this));
        fsm.AddState(StateName.FlyingEvade, new State_FlyingEvade(this, fsm, StateName.FlyingCharge));
        fsm.AddState(StateName.Idle, new State_Idle(this, fsm, StateName.FlyingEvade));

        fsm.ChangeState(StateName.Idle);
    }
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<IDamageable>();
        if (player != null)
        {
            player.TakeDamage(_dmg);
            Die();
        }
    }
    public override void Die()
    {
        Destroy(gameObject);
    }
}
