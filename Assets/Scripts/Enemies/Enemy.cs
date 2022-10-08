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


    [Header("Movement")]
    [SerializeField] private float knockBackTime = .3f;
    public bool canMove = true;
    protected bool isAlive = true;
    protected bool isFacingRight = true;

    public IMovement slowMovement;
    public IMovement fastMovement;

    protected Action EnemyOnUpdate;
    public StateMachine fsm;
    public Rigidbody myRb;
    [SerializeField] GameObject _myModel;
    [SerializeField] protected Collider myCollider;
    [HideInInspector] public Animator myAnim;

    [Header("Ranges Movement")]
    public float viewRange;

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
    public void LookAtPlayer()
    {

        if(GameManager.instance.Player.transform.position.x > transform.position.x)
        {
            isFacingRight = true;
            _myModel.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            isFacingRight = false;
            _myModel.transform.rotation = Quaternion.Euler(0, 180, 0);
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
        if (isFacingRight) _myModel.transform.rotation = Quaternion.Euler(0, 180, 0);
        else _myModel.transform.rotation = Quaternion.Euler(0, 0, 0);

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
