using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : Entity
{
    public float attackDmg;

    [SerializeField] float _distanceToFunction = 25f;

    [Header("Movement")]
    [SerializeField] private float knockBackTime = .3f;
    public bool canMove = true;
    protected bool isAlive = true;
    [SerializeField] protected bool isFacingRight = true;

    protected Action EnemyOnUpdate;
    public StateMachine fsm;
    public Rigidbody myRb;
    [SerializeField] protected Collider myCollider;
    [HideInInspector] public Animator myAnim;

    private void Awake()
    {
        fsm = new StateMachine();
        myRb = GetComponent<Rigidbody>();
        myAnim = GetComponentInChildren<Animator>();
    }
    public virtual void Update()
    {
        if (canMove && isAlive && GameManager.instance.GetDirectionToPlayer(this.transform).magnitude < _distanceToFunction) fsm.Update();
    }
    public virtual void FixedUpdate()
    {
        if (canMove && isAlive && GameManager.instance.GetDirectionToPlayer(this.transform).magnitude < _distanceToFunction) fsm.FixedUpdate();

    }
    public virtual void LookAtPlayer()
    {

        if(GameManager.instance.Player.transform.position.x > transform.position.x)
        {
            isFacingRight = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            isFacingRight = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }

    public bool CanSeePlayer()
    {
        return !Physics.Raycast(transform.position,
                                  GameManager.instance.GetDirectionToPlayer(this.transform).normalized,
                                 (GameManager.instance.GetDirectionToPlayer(this.transform) - transform.position).magnitude,
                                  GameManager.instance.WallLayer);
    }
    public void Flip()
    {
        if (isFacingRight) transform.rotation = Quaternion.Euler(0, 0, 0);
        else transform.rotation = Quaternion.Euler(0, 180, 0);

        isFacingRight = !isFacingRight;
    }

    public override void TakeDamage(float dmg)
    {
        _currentLife -= dmg;
        if (_currentLife <= 0) Die();
        else KnockBack();
    }

    public override void Die()
    {
        isAlive = false;
        myRb.isKinematic = true;
        myCollider.enabled = false;
    }
    private void KnockBack()
    {
        canMove = false;
        StartCoroutine(ResetCanMove(knockBackTime));
    }
    IEnumerator ResetCanMove(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}
