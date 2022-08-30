using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rb;

    [Header("Movement Variables")]
    [SerializeField] float _movementSpeed = 9;
    [SerializeField] float _acceleration = 9;
    [SerializeField] float _decceleration = 9;
    [SerializeField] float _velPower = 1.2f;
    [SerializeField] float _frictionAmount = .1f;

    [Header("Jump Variables")]
    [SerializeField] float _jumpForce = 12;
    [SerializeField] float _doubleJumpForce = 13;
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

    [SerializeField] LayerMask _groundLayer;

    IController _myController;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        PlayerModel _playerModel = new PlayerModel(transform, _rb, _groundLayer, _frictionAmount, _movementSpeed, _acceleration, _decceleration, _velPower,
            _jumpCutMultiplier, _jumpForce, _doubleJumpForce, _dashForce, _dashTime, _dashCooldown, _jumpBufferLength, _jumpCoyotaTime, _gravityScale, _fallGravityMultiplier);
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
