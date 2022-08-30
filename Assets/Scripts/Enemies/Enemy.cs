using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float viewRange;
    public float attackRange;
    public float speed;

    public IMovement myMovement;
    public StateMachine fsm;

    private void Awake()
    {
        fsm = fsm = new StateMachine();
    }
    public void LookAtPlayer()
    {
        transform.right = GameManager.instance.GetDistanceToPlayer(this.transform).normalized;
    }
    public bool CanSeePlayer()
    {
        return !Physics2D.Raycast(transform.position, 
                                  GameManager.instance.GetDistanceToPlayer(this.transform).normalized, 
                                 (GameManager.instance.GetDistanceToPlayer(this.transform) - transform.position).magnitude,
                                  GameManager.instance.wallLayer);
    }
}
