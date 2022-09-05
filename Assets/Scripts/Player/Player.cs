using UnityEngine;
public class Player : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] float _movementSpeed = 9;
    [SerializeField] float _acceleration = 9;
    [SerializeField] float _decceleration = 9;
    [SerializeField] float _velPower = 1.2f;
    [SerializeField] float _frictionAmount = .1f;

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
    [SerializeField] float _gravityScale = 1.4f;
    [SerializeField] float _fallGravityMultiplier = 1.9f;

    [Header("Pogo Variables")]
    [SerializeField] float _pogoForce;

    [SerializeField] LayerMask _groundLayer;

    IController _myController;
    void Start()
    {
        PlayerModel _playerModel = new PlayerModel(transform, GetComponent<Rigidbody>(), _groundLayer, _frictionAmount, _movementSpeed, _acceleration, _decceleration, _velPower,
            _jumpCutMultiplier, _jumpForce, _dashForce, _dashTime, _dashCooldown, _jumpBufferLength, _jumpCoyotaTime, _gravityScale, _fallGravityMultiplier, _pogoForce);

        _myController = new PlayerController(_playerModel, this);
    }
    void Update()
    {
        _myController.OnUpdate();
    }
    private void FixedUpdate()
    {
        _myController.OnFixedUpdate();
    }
}
