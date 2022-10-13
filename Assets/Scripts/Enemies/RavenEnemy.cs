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

    [SerializeField] Particle _deadParticle;

    protected override void Start()
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
        var damageable = other.GetComponent<IDamageable>();
        var player = other.GetComponent<Player>();
        if (damageable != null)
        {
            damageable.TakeDamage(_dmg);
            if (player)
                player.Knockback(transform.position.x);

            Die();
        }
    }
    public override void Die()
    {
        Instantiate(_deadParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
