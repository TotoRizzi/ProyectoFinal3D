using UnityEngine;
public class Player : Entity
{
    [Header("Movement Variables")]
    [SerializeField] float _movementSpeed = 9;
    [SerializeField] float _acceleration = 12;
    [SerializeField] float _decceleration = 15;
    [SerializeField] float _velPower = 1.2f;
    [SerializeField] float _frictionAmount = .1f;
    [SerializeField] float _maxFoce = .1f;

    [Header("Jump Variables")]
    [SerializeField] float _jumpForce = 12;
    [SerializeField] float _jumpCutMultiplier = .08f;
    [SerializeField] float _jumpCoyotaTime = .1f;
    [SerializeField] float _jumpBufferLength = .1f;

    [Header("Dash Variables")]
    [SerializeField] float _dashForce = 20;
    [SerializeField] float _dashTime = .15f;
    [SerializeField] float _dashCooldown = 1;

    [Header("Gravity")]
    [SerializeField] float _gravityScale = 1.5f;
    [SerializeField] float _fallGravityMultiplier = 1.9f;

    [Header("Pogo Variables")]
    [SerializeField] float _pogoForce = 12;
    [SerializeField] float _timeToPogo;

    [Header("Attack Variables")]
    [SerializeField] float _timeToAttack = 1f;

    [SerializeField] LayerMask _groundLayer;

    IController _myController;
    PlayerView _playerView;
    override protected void Start()
    {
        base.Start();
        PlayerModel _playerModel = new PlayerModel(transform, GetComponent<Rigidbody>(), _groundLayer, _frictionAmount, _movementSpeed, _acceleration, _decceleration, _velPower,
            _jumpCutMultiplier, _jumpForce, _dashForce, _dashTime, _dashCooldown, _jumpBufferLength, _jumpCoyotaTime, _gravityScale, _fallGravityMultiplier, _pogoForce, _timeToAttack,
            _timeToPogo);
        _playerView = new PlayerView(GetComponent<Animator>(), GetComponentInChildren<Renderer>().material);
        _myController = new PlayerController(_playerModel, this);

        _playerModel.runAction += _playerView.RunAnimation;

        _playerModel.inGrounded += _playerView.InGrounded;

        _playerModel.jumpAction += _playerView.JumpAnimation;

        _playerModel.fallingAction += _playerView.FallingAnimation;

        _playerModel.dashAction += _playerView.DashAnimation;

        _playerModel.attackAction += _playerView.AttackAnimation;
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
}
