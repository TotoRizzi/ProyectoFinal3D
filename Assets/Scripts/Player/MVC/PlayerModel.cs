using System;
using System.Collections;
using UnityEngine;
public class PlayerModel
{
    float _xAxis;
    bool _isJumping;
    bool _canDoubleJump;
    bool _dashing;
    bool _canDash = true;
    bool _poging;
    bool _attacking;
    float _coyotaTimer;
    float _bufferTimer;

    //Variables Constructor
    Transform _transform;
    Rigidbody _rb;
    LayerMask _groundLayer;
    float _frictionAmount;
    float _movementSpeed;
    float _acceleration;
    float _decceleration;
    float _velPower;
    float _jumpCutMultiplier;
    float _jumpForce;
    float _dashForce;
    float _dashTime;
    float _dashCoolDown;
    float _jumpBufferLength;
    float _jumpCoyotaTime;
    float _gravityScale;
    float _fallGravityMultiplier;
    float _pogoForce;
    float _timeToAttack;
    float _timeToPogo;
    bool _inGrounded => Physics.CheckSphere(_transform.position, 0.1f, _groundLayer);
    bool _canJump => _bufferTimer > 0 && _coyotaTimer > 0 && !_isJumping;

    public Action<float> runAction;
    public Action jumpAction;
    public Action<bool> fallingAction;
    public Action dashAction;
    public Action attackAction;
    public Action<bool> inGrounded;
    public PlayerModel(Transform transform, Rigidbody rb, LayerMask groundLayer, float frictionAmount, float movementSpeed, float acceleration, float decceleration,
        float velPower, float jumpCutMultiplier, float jumpForce, float dashForce, float dashTime, float dashCoolDown, float jumpBufferLength,
        float jumpCoyotaTime, float gravityScale, float fallGravityMultiplier, float pogoForce, float attackCooldown, float timeToPogo)
    {
        _transform = transform;
        _rb = rb;
        _groundLayer = groundLayer;
        _frictionAmount = frictionAmount;
        _movementSpeed = movementSpeed;
        _acceleration = acceleration;
        _decceleration = decceleration;
        _velPower = velPower;
        _jumpCutMultiplier = jumpCutMultiplier;
        _jumpForce = jumpForce;
        _dashForce = dashForce;
        _dashTime = dashTime;
        _dashCoolDown = dashCoolDown;
        _jumpBufferLength = jumpBufferLength;
        _jumpCoyotaTime = jumpCoyotaTime;
        _gravityScale = gravityScale;
        _fallGravityMultiplier = fallGravityMultiplier;
        _pogoForce = pogoForce;
        _timeToAttack = attackCooldown;
        _timeToPogo = timeToPogo;
    }
    public void OnUpdate(float xAxis)
    {
        _xAxis = xAxis;

        _bufferTimer -= Time.deltaTime;

        if (_canJump)
        {
            Jump();
            _bufferTimer = 0;
        }

        if (_inGrounded)
        {
            _coyotaTimer = _jumpCoyotaTime;
            _poging = false;
        }
        else
            _coyotaTimer -= Time.deltaTime;

        #region JumpChecks
        if (_isJumping && _rb.velocity.y < 0)
            _isJumping = false;
        #endregion

        Falling(!_inGrounded && _rb.velocity.y < 0);

        inGrounded(_inGrounded);
    }
    public void OnFixedUpdate()
    {
        //Flipeo el sprite del player teniendo en cuenta la posicion del mouse
        if (_xAxis != 0 && !_attacking)
            _transform.rotation = _xAxis <= 0 ? Quaternion.Euler(0, -90, 0) : Quaternion.Euler(0, 90, 0);

        Friction();
        Run();

        if (!_canDash && _rb.velocity.magnitude > _dashForce)
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _dashForce);
    }
    void Friction()
    {
        if (Mathf.Abs(_xAxis) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(_frictionAmount));

            amount *= Mathf.Sign(_rb.velocity.x);
            _rb.AddForce(Vector3.right * -amount, ForceMode.Impulse);
        }
    }
    public void Run()
    {
        if (_dashing || _poging || _attacking) return;

        if (_inGrounded)
            runAction(_xAxis);

        float targetSpeed = _xAxis * _movementSpeed;

        float speedDif = targetSpeed - _rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.5f) ? _acceleration : _decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velPower) * Mathf.Sign(speedDif);

        _rb.AddForce(movement * Vector3.right);
    }
    void Jump()
    {
        if (_dashing || _poging || _attacking) return;
        _isJumping = true;

        _rb.AddForce(Vector3.up * (_jumpForce - _rb.velocity.y), ForceMode.Impulse);
        jumpAction();
        _poging = false;
    }
    public void OnJumpUp()
    {
        if (_rb.velocity.y > 0 && _isJumping)
            _rb.AddForce(Vector3.down * _rb.velocity.y * (1 - _jumpCutMultiplier), ForceMode.Impulse);
    }
    public void OnJumpDown()
    {
        _bufferTimer = _jumpBufferLength;

        if (!_canDoubleJump && _coyotaTimer > 0)
            _canDoubleJump = true;
        else if (_canDoubleJump && _coyotaTimer < 0)
        {
            _coyotaTimer = _jumpCoyotaTime;
            _canDoubleJump = false;
        }
    }
    public IEnumerator Dash(float xAxis, float yAxis)
    {
        if (_canDash && xAxis != 0 && !_attacking)
        {
            dashAction();
            _canDash = false;
            _dashing = true;
            _poging = false;
            _rb.useGravity = false;
            _rb.AddForce(new Vector3(xAxis, yAxis, 0).normalized * _dashForce, ForceMode.Impulse);
            yield return new WaitForSeconds(_dashTime);
            _dashing = false;
            _rb.useGravity = true;
            yield return new WaitForSeconds(_dashCoolDown);
            _canDash = true;
        }
    }
    public IEnumerator Pogo(float xAxis, float yAxis)
    {
        Vector3 pogoDirection = new Vector3(xAxis * _pogoForce + _rb.velocity.x / 2, yAxis * _pogoForce + _rb.velocity.y, 0);

        if (!_inGrounded && !_poging && yAxis < 0)
        {
            _poging = true;
            _rb.AddForce(-pogoDirection, ForceMode.Impulse);
            yield return new WaitForSeconds(_timeToPogo);
            _poging = false;
        }
        _isJumping = false;
    }
    public void Falling(bool falling)
    {
        fallingAction(falling);
        if (falling)
            _rb.AddForce(Vector3.down * (_gravityScale * _fallGravityMultiplier));
        else
            _rb.AddForce(Vector3.down * _gravityScale);
    }

    public IEnumerator Attack()
    {
        if (!_attacking)
        {
            _attacking = true;
            attackAction();
            yield return new WaitForSeconds(_timeToAttack);
            _attacking = false;
        }
    }
}
