using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGroundEnemy : Enemy
{
    [SerializeField] float _mySpeed;
    [SerializeField] Transform[] _myWaypoints;
    [SerializeField] Transform _myGroundCheck;

    public IMovement _myMovement;
    protected override void Start()
    {
        _myMovement = new WayPointMovement(transform, myRb, _mySpeed, _myWaypoints, _myGroundCheck);

        fsm.AddState(StateName.WayPointWalk, new State_WayPointWalk(this));
        fsm.ChangeState(StateName.WayPointWalk);
    }
    public override void Die()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider collider)
    {
        var player = collider.GetComponent<Player>();
        if (player)
        {
            player.TakeDamage(attackDmg);
            player.Knockback(transform.position.x);
        }
    }
}
