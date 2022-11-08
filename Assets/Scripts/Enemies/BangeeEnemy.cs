using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangeeEnemy : Enemy
{
    [SerializeField] Transform _myModel;
    [SerializeField] GameObject _screamModel;

    [Header("Speeds")]
    [SerializeField] float _directionalSpeed = .1f;
    [SerializeField] float _circleSpeed = .5f;
    public float wideness = 2;

    public float attackSpeed = 5;
    float _currentAttackSpeed;

    [Header("Ranges")]
    public float sightRange;
    public float circleRange;
    public float stopCircleRange;
    [SerializeField] float _screamRange;
    [SerializeField] float fixedRotationPositionRight = 170.0f;
    [SerializeField] float fixedRotationPositionLeft = 215.0f;

    public IMovement directionalMovement;
    public IMovement circleMovement;

    protected override void Start()
    {
        base.Start();

        directionalMovement = new DirectedMovement(transform, myRb, _directionalSpeed, GameManager.instance.Player.transform);
        circleMovement = new CircleMovement(transform, myRb, _circleSpeed, wideness);

        fsm.AddState(StateName.FollowPlayer, new State_FollowPlayer(this, fsm));
        fsm.AddState(StateName.CirclePlayer, new State_CirclePlayer(this, fsm));

        fsm.ChangeState(StateName.FollowPlayer);
    }
    public override void Update()
    {
        base.Update();
        LookAtPlayer();
        _currentAttackSpeed += Time.deltaTime;

        if (_currentAttackSpeed >= attackSpeed) Attack();
    }
    public override void LookAtPlayer()
    {
        if (GameManager.instance.Player.transform.position.x > transform.position.x)
        {
            isFacingRight = false;
            _myModel.rotation = Quaternion.Euler(0, fixedRotationPositionRight, 0);
        }
        else
        {
            isFacingRight = true;
            _myModel.rotation = Quaternion.Euler(0, fixedRotationPositionLeft, 0);
        }
    }
    void Attack()
    {
        StartCoroutine(Scream());
    }
    IEnumerator Scream()
    {
        canMove = false;
        _screamModel.SetActive(true);
        
        yield return new WaitForSeconds(1f);

        _currentAttackSpeed = 0;
        _screamModel.SetActive(false);
        canMove = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, _screamRange);
    }
}
