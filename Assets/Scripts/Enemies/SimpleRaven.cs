using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRaven : Enemy
{
    [SerializeField] GameObject _myModel;

    public float maxMovingForce;
    public float chargeSpeed;

    public IMovement targetMovement;

    protected override void Start()
    {
        targetMovement = new DirectedMovement(transform, myRb, chargeSpeed, GameManager.instance.Player.transform);

        fsm.AddState(StateName.FlyingCharge, new State_FlyingCharge(this));

        fsm.ChangeState(StateName.FlyingCharge);
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        var player = other.GetComponent<Player>();
        if (damageable != null)
        {
            damageable.TakeDamage(attackDmg);
            if (player)
                player.Knockback(transform.position.x);

            Die();
        }
    }
    public override void Die()
    {
        FRY_DeadRavenParticle.Instance.pool.GetObject().SetPosition(transform.position);
    }

    public override void LookAtPlayer()
    {
        if (GameManager.instance.Player.transform.position.x > transform.position.x)
        {
            isFacingRight = false;
             _myModel.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            isFacingRight = true;
            _myModel.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
