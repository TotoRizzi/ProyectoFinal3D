using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;

    public float attackDmg;

    [Header("Ranges Movement")]
    public float viewRange;
    public float attackRange;

    [Header("Movement")]
    [SerializeField] private float knockBackTime = .3f;
    public bool canMove = true;
    protected bool isAlive = true;
    private bool isFacingRight = true;

    public IMovement walkingMovement;
    public IMovement chasingMovement;

    protected Action EnemyOnUpdate;
    public StateMachine fsm;
    protected Rigidbody myRb;
    [SerializeField] protected BoxCollider myCollider;
    [HideInInspector] public Animator myAnim;

    private void Awake()
    {
        fsm = new StateMachine();
        myRb = GetComponent<Rigidbody>();
        myAnim = GetComponentInChildren<Animator>();
    }
    public virtual void Start()
    {
        currentHp = maxHp;
    }
    public virtual void Update()
    {
        if (canMove && isAlive) fsm.Update();
    }
    public virtual void FixedUpdate()
    {
        if (canMove && isAlive) fsm.FixedUpdate();

    }
    public void LookAtPlayer(bool imFlying)
    {
        if (imFlying)
        {
            var dir = GameManager.instance.GetDirectionToPlayer(this.transform);
            dir.z = 0;
            transform.right = dir.normalized;
        }
        else
        {
            if(GameManager.instance.Player.transform.position.x > transform.position.x)
            {
                isFacingRight = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                isFacingRight = false;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
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
        if (isFacingRight) transform.rotation = Quaternion.Euler(0, 180, 0);
        else transform.rotation = Quaternion.Euler(0, 0, 0);

        isFacingRight = !isFacingRight;
    }

    public void TakeDamage(float dmg)
    {
        currentHp -= dmg;
        if (currentHp <= 0) Die();
        else KnockBack();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
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
