using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Player : Entity
{
    [Header("Movement Variables")]
    [SerializeField] float _movementSpeed = 8;
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
    [SerializeField] float _dashForce = 20;
    [SerializeField] float _dashTime = .15f;
    [SerializeField] float _dashCooldown = 1;

    [Header("Gravity")]
    [SerializeField] float _gravityScale = 1.4f;
    [SerializeField] float _fallGravityMultiplier = 1.9f;

    [Header("Pogo Variables")]
    [SerializeField] float _pogoForce = 15;

    [Header("Attack Variables")]
    [SerializeField] float _attackRate = .36f;
    [SerializeField] float _timeToThrow = .45f;
    [SerializeField] float _boomerangSpearDistance;

    [Header("Stamina Variables")]
    [SerializeField] float _maxStamina;
    [SerializeField] float _meleeAttackStamina;
    [SerializeField] float _throwSpearStamina;
    [SerializeField] float _jumpStamina;
    [SerializeField] float _timeToAddStamina;

    [Header("Knockback Variables")]
    [SerializeField] float _knockbackForce;
    [SerializeField] float _invulnerabilityTime;

    [Header("Inspector Variables")]
    [SerializeField] ParticleSystem _doubleJumpPS;
    [SerializeField] ParticleSystem _pogoPS;
    [SerializeField] BoomerangSpear _boomerangSpearPrefab;
    [SerializeField] Transform _spawnSpear;
    [SerializeField] Image _staminaFill;
    [SerializeField] Image _hpFill;
    [SerializeField] Image _invulneravilityImg;

    IController _myController;
    PlayerView _playerView;
    PlayerSpear _playerSpear;
    BoomerangSpear _throwedSpear;
    InputManager _inputManager;

    System.Action<float> updateLifeBar;
    public System.Action getDamage;
    override protected void Start()
    {
        base.Start();
        _inputManager = FindObjectOfType<InputManager>();
        Rigidbody _rb = GetComponent<Rigidbody>();
        _playerSpear = GetComponentInChildren<PlayerSpear>();
        PlayerModel _playerModel = new PlayerModel(transform, _rb, _groundFriction, _movementSpeed, _acceleration, _decceleration, _velPower,
            _jumpCutMultiplier, _jumpForce, _dashForce, _dashTime, _dashCooldown, _jumpBufferLength, _jumpCoyotaTime, _gravityScale, _fallGravityMultiplier, _pogoForce,
            _attackRate, _timeToThrow, _maxStamina, _meleeAttackStamina, _throwSpearStamina, _jumpStamina, _timeToAddStamina, _knockbackForce, _playerSpear);
        _playerView = new PlayerView(GetComponent<Animator>(), GetComponentInChildren<Renderer>().material, _doubleJumpPS, _pogoPS, _staminaFill, _hpFill,
            _invulneravilityImg, _invulnerabilityTime, GetComponentInChildren<TrailRenderer>());
        _myController = new PlayerController(_playerModel, this, _inputManager);

        _playerModel.runAction += _playerView.RunAnimation;

        _playerModel.inGroundedAction += _playerView.InGrounded;

        _playerModel.jumpAction += _playerView.JumpAnimation;

        _playerModel.fallingAction += _playerView.FallingAnimation;

        _playerModel.dashAction += _playerView.DashAnimation;

        _playerModel.attackAction += _playerView.AttackAnimation;

        _playerModel.throwAnimation += _playerView.ThrowAnimation;

        _playerModel.pogoFeedback += _playerView.PogoFeedback;

        _playerModel.updateStamina += _playerView.UpdateStaminaBar;

        updateLifeBar += _playerView.UpdateLifeBar;

        getDamage += () => StartCoroutine(_playerView.TakeDamageFeedback());
        getDamage += () => StartCoroutine(_playerModel.HitPlayer());
    }
    void Update()
    {
        _myController.OnUpdate();
        _playerView.OnUpdate();
    }
    private void FixedUpdate()
    {
        _myController.OnFixedUpdate();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * 5);
    }
    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        updateLifeBar(_currentLife / _maxLife);
        getDamage();
    }
    public override void Die()
    {
        Debug.Log("Player ha muerto");
        //SceneManagerScript.instance.ReloadScene();
    }
    public void InstantiateSpear()
    {
        _playerSpear.gameObject.SetActive(false);
        _throwedSpear = Instantiate(_boomerangSpearPrefab).SetPosition(_spawnSpear)
                                                          .SetDirection(_spawnSpear.position + transform.forward * _boomerangSpearDistance);
        _throwedSpear.playerSpear = _playerSpear;
    }
    public void MoveToSpear()
    {
        if (_throwedSpear)
        {
            transform.position = new Vector3(_throwedSpear.transform.position.x, _throwedSpear.transform.position.y, transform.position.z) - Vector3.up;
            _playerSpear.ActiveSpear(_throwedSpear);
        }
    }
}
