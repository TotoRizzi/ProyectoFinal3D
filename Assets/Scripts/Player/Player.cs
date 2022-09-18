using UnityEngine;
public class Player : Entity
{
    [Header("Movement Variables")]
    [SerializeField] float _movementSpeed = 9;
    [SerializeField] float _acceleration = 12;
    [SerializeField] float _decceleration = 12;
    [SerializeField] float _groundFriction = .1f;
    [SerializeField] float _velPower = 1.05f;

    [Header("Jump Variables")]
    [SerializeField] float _jumpForce = 12;
    [SerializeField] float _jumpCutMultiplier = .1f;
    [SerializeField] float _jumpCoyotaTime = .1f;
    [SerializeField] float _jumpBufferLength = .1f;

    [Header("Dash Variables")]
    [SerializeField] float _dashForce = 25;
    [SerializeField] float _dashTime = .15f;
    [SerializeField] float _dashCooldown = 1;

    [Header("Gravity")]
    [SerializeField] float _gravityScale = 1.5f;
    [SerializeField] float _fallGravityMultiplier = 1.9f;

    [Header("Pogo Variables")]
    [SerializeField] float _pogoForce = 15;

    [Header("Attack Variables")]
    [SerializeField] float _timeToAttack = .28f;
    [SerializeField] float _timeToThrow = .22f;

    [Header("Inspector Variables")]
    [SerializeField] ParticleSystem _doubleJumpPS;
    [SerializeField] ParticleSystem _pogoPS;
    [SerializeField] Spear _spearPrefab;
    [SerializeField] Transform _spawnSpear;

    IController _myController;
    PlayerView _playerView;
    Spear _playerSpear;
    override protected void Start()
    {
        base.Start();
        _playerSpear = GetComponentInChildren<Spear>();
        PlayerModel _playerModel = new PlayerModel(transform, GetComponent<Rigidbody>(), _groundFriction, _movementSpeed, _acceleration, _decceleration, _velPower,
            _jumpCutMultiplier, _jumpForce, _dashForce, _dashTime, _dashCooldown, _jumpBufferLength, _jumpCoyotaTime, _gravityScale, _fallGravityMultiplier, _pogoForce,
            _timeToAttack, _timeToThrow, _playerSpear);
        _playerView = new PlayerView(GetComponent<Animator>(), GetComponentInChildren<Renderer>().material, _doubleJumpPS, _pogoPS);
        _myController = new PlayerController(_playerModel, this);

        _playerModel.runAction += _playerView.RunAnimation;

        _playerModel.inGroundedAction += _playerView.InGrounded;

        _playerModel.jumpAction += _playerView.JumpAnimation;

        _playerModel.fallingAction += _playerView.FallingAnimation;

        _playerModel.dashAction += _playerView.DashAnimation;

        _playerModel.attackAction += _playerView.AttackAnimation;

        _playerModel.throwAnimation += _playerView.ThrowAnimation;

        _playerModel.throwAction += InstantiateSpear;

        //_playerModel.pogoAnimation += _playerView.PogoAnimation;

        //_playerModel.pogoFeedback += _playerView.PogoFeedback;
    }
    void Update()
    {
        _myController.OnUpdate();
    }
    private void FixedUpdate()
    {
        _myController.OnFixedUpdate();
    }
    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        StartCoroutine(_playerView.TakeDamageFeedback());
    }
    public void InstantiateSpear(float xAxis)
    {
        _playerSpear.gameObject.SetActive(false);
        Instantiate(_spearPrefab).OnThrow()
                                 .SetPosition(_spawnSpear.transform)
                                 .SetSpeed(20)
                                 .SetDirection(transform.forward)
                                 .SetCollider();
    }
}
