using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    //Ranges
    public float viewRange;
    public float attackRange;

    //Movement
    [SerializeField] protected float speed = 1;
    [SerializeField] private float knockBackTime = .3f;
    public bool canMove = true;
    private bool isFacingRight = true;

    public IMovement myMovement;

    public StateMachine fsm;
    protected Rigidbody myRb;
    protected Animator myAnim;

    //Health
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;

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
        if (canMove) fsm.Update();
    }
    public virtual void FixedUpdate()
    {
        if (canMove) fsm.FixedUpdate();

    }
    public void LookAtPlayer()
    {
        var dir = GameManager.instance.GetDistanceToPlayer(this.transform);
        dir.z = 0;
        transform.right = dir.normalized;
    }
    public bool CanSeePlayer()
    {
        return !Physics.Raycast(transform.position,
                                  GameManager.instance.GetDistanceToPlayer(this.transform).normalized,
                                 (GameManager.instance.GetDistanceToPlayer(this.transform) - transform.position).magnitude,
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

    public void Die()
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
