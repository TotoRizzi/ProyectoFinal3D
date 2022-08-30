using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float viewRange;
    public float attackRange;

    [SerializeField] protected float speed;

    public bool canMove = true;
    private bool isFacingRight = true;

    public IMovement myMovement;
    public StateMachine fsm;

    protected Rigidbody myRb;

    private void Awake()
    {
        fsm = new StateMachine();
        myRb = GetComponent<Rigidbody>();
    }
    public virtual void Start()
    {
        
    }
    public virtual void Update()
    {
        if (canMove) fsm.Update();
    }
    public void LookAtPlayer()
    {
        transform.right = GameManager.instance.GetDistanceToPlayer(this.transform).normalized;
    }
    public bool CanSeePlayer()
    {
        return !Physics.Raycast(transform.position, 
                                  GameManager.instance.GetDistanceToPlayer(this.transform).normalized, 
                                 (GameManager.instance.GetDistanceToPlayer(this.transform) - transform.position).magnitude,
                                  GameManager.instance.wallLayer);
    }
    public void Flip()
    {
        if (isFacingRight) transform.rotation = Quaternion.Euler(0, 180, 0);
        else Quaternion.Euler(0, 0, 0);

        isFacingRight = !isFacingRight;
    }
}
