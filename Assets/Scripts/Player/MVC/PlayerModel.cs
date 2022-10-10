using System;
using System.Collections;
using UnityEngine;
public class PlayerModel
{
    float _currentStamina;
    float _staminaTimer;
    bool _isJumping;
    bool _canDoubleJump;
    bool _dashing;
    bool _canDash = true;
    bool _poging;
    bool _attacking;
    bool _jumpFalling;
    bool _gettingHit;
    public bool onJumpUp;
    float _coyotaTimer;
    float _bufferTimer;
    float _timeToAttack;

    //Variables Constructor
    Transform _transform;
    Rigidbody _rb;
    float _groundFriction;
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
    float _attackRate;
    float _boomerangSpearDistance;
    float _maxStamina;
    float _attackStamina;
    float _throwSpearStamina;
    float _jumpStamina;
    float _timeToAddStamina;
    float _knockbackForce;
    public PlayerSpear playerSpear;
    public bool inGrounded => Physics.CheckSphere(_transform.position, 0.1f, GameManager.instance.GroundLayer);
    bool _canJump => _bufferTimer > 0 && _coyotaTimer > 0 && !_isJumping;

    public Action<float> runAction;
    public Action<bool> jumpAction;
    public Action<bool> fallingAction;
    public Action<bool> dashAction;
    public Action<bool> inGroundedAction;
    public Action pogoFeedback;
    public Action throwAnimation;
    public Action<int> attackAction;
    public Action<float> updateStamina;
    public PlayerModel(Transform transform, Rigidbody rb, float groundFriction, float movementSpeed, float acceleration,
        float decceleration, float velPower, float jumpCutMultiplier, float jumpForce, float dashForce, float dashTime, float dashCoolDown,
        float jumpBufferLength, float jumpCoyotaTime, float gravityScale, float fallGravityMultiplier, float pogoForce, float attackRate, float boomerangSpearDistance,
        float maxStamina, float attackStamina, float throwSpearStamina, float jumpStamina, float timeToAddStamina, float knockbackForce, PlayerSpear playerSpear)
    {
        _transform = transform;
        _rb = rb;
        _groundFriction = groundFriction;
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
        _attackRate = attackRate;
        _boomerangSpearDistance = boomerangSpearDistance;
        _maxStamina = maxStamina;
        _attackStamina = attackStamina;
        _throwSpearStamina = throwSpearStamina;
        _jumpStamina = jumpStamina;
        _timeToAddStamina = timeToAddStamina;
        _knockbackForce = knockbackForce;
        this.playerSpear = playerSpear;

        _currentStamina = _maxStamina;
    }
    public void OnUpdate()
    {
        _bufferTimer -= Time.deltaTime;
        _staminaTimer -= Time.deltaTime;

        if (inGrounded)
        {
            _coyotaTimer = _jumpCoyotaTime;
            _jumpFalling = true;
            _canDoubleJump = false;
            _poging = false;
        }
        else
            _coyotaTimer -= Time.deltaTime;

        #region JumpChecks
        if (_isJumping && _rb.velocity.y < 0)
            _isJumping = false;

        if (_poging && _rb.velocity.y < 0)
            _poging = false;
        #endregion

        fallingAction(_rb.velocity.y < 0 && !inGrounded);
        inGroundedAction(inGrounded);

        AddStamina();
    }
    public void OnFixedUpdate(float xAxis)
    {
        if (_dashing || _gettingHit) return;

        if (xAxis != 0)
            _transform.eulerAngles = Vector3.Lerp(Quaternion.Euler(0, 90, 0).eulerAngles, Quaternion.Euler(0, -90, 0).eulerAngles, 0) * xAxis;

        GroundFriction(xAxis);
        Run(xAxis);
        Falling(!inGrounded);

        if (!_canDash && _rb.velocity.magnitude > _dashForce)
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _dashForce);

        if (_poging && _rb.velocity.y > _pogoForce)
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _pogoForce);

        if (_canJump)
        {
            Jump();
            _bufferTimer = 0;
        }

        if (onJumpUp)
            OnJumpUp();
    }
    void GroundFriction(float xAxis)
    {
        if (Mathf.Abs(xAxis) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(_groundFriction));
            amount *= Mathf.Sign(_rb.velocity.x);
            _rb.AddForce(Vector3.right * -amount, ForceMode.Impulse);
        }
    }
    public void Run(float xAxis)
    {
        if (inGrounded)
            runAction(xAxis);

        float targetSpeed = xAxis * _movementSpeed;

        float speedDif = targetSpeed - _rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.5f) ? _acceleration : _decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velPower) * Mathf.Sign(speedDif);

        float finalMovement = inGrounded ? movement : movement * 0.75f;

        _rb.AddForce(Vector3.right * finalMovement);
    }
    void Jump()
    {
        if (_dashing || _currentStamina < _jumpStamina) return;
        _rb.AddForce(Vector3.up * (_jumpForce - _rb.velocity.y), ForceMode.Impulse);
        jumpAction(_canDoubleJump || _jumpFalling);
        SubstactStamina(_jumpStamina);
        _isJumping = true;
        _poging = false;
    }
    public void OnJumpUp()
    {
        if (_rb.velocity.y < 0 || _poging) return;
        _rb.AddForce(Vector3.down * _rb.velocity.y * (1 - _jumpCutMultiplier), ForceMode.Impulse);
    }
    public void OnJumpDown()
    {
        _bufferTimer = _jumpBufferLength;
        onJumpUp = false;

        //Para el doble salto
        //if (!_canDoubleJump)
        //{
        //    if (inGrounded)                     //Primer salto desde el suelo
        //    {
        //        _canDoubleJump = true;
        //        _jumpFalling = false;
        //    }
        //    else if (_coyotaTimer > 0)          //Primer salto con coyota time
        //        _canDoubleJump = true;
        //    else if (_jumpFalling)              //Salto sin estar en el suelo
        //    {
        //        _coyotaTimer = _jumpCoyotaTime;
        //        _jumpFalling = false;
        //    }
        //}
        //else                                    //Doble salto
        //{
        //    _coyotaTimer = _jumpCoyotaTime;
        //    _canDoubleJump = false;
        //    _jumpFalling = false;
        //}
    }
    public IEnumerator Dash(float xAxis, float yAxis)
    {
        if (_canDash && xAxis != 0 && !_attacking)
        {
            dashAction(_dashing = true);
            _canDash = false;
            _poging = false;
            _rb.useGravity = false;
            _rb.AddForce(new Vector3(xAxis, yAxis, 0).normalized * _dashForce, ForceMode.Impulse);
            yield return new WaitForSeconds(_dashTime);
            dashAction(_dashing = false);
            _rb.useGravity = true;
            yield return new WaitForSeconds(_dashCoolDown);
            _canDash = true;
        }
    }
    public void Pogo(float yAxis)
    {
        Vector3 pogoDirection = new Vector3(0, yAxis, 0);
        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.AddForce(-pogoDirection * (_pogoForce - _rb.velocity.y), ForceMode.Impulse);
            _poging = true;
            pogoFeedback();
        }
    }
    public void Falling(bool falling)
    {
        if (falling)
            _rb.AddForce(Vector3.down * (_gravityScale * _fallGravityMultiplier));
        else
            _rb.AddForce(Vector3.down * _gravityScale);
    }

    public void Attack(float yAxis)
    {
        if (_currentStamina < _attackStamina || !playerSpear.canUseSpear) return;

        if (Time.time >= _timeToAttack)
        {
            _attacking = true;
            _timeToAttack = Time.time + 1 / _attackRate;
            attackAction((int)yAxis);
            SubstactStamina(_attackStamina);
        }
        _attacking = false;
    }
    public void Throw()
    {
        if (_currentStamina < _throwSpearStamina || !playerSpear.canUseSpear) return;

        playerSpear.canUseSpear = false;
        throwAnimation();
        SubstactStamina(_throwSpearStamina);
    }
    public IEnumerator HitPlayer()
    {
        _gettingHit = true;
        _poging = false;
        _isJumping = false;
        _rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(.1f);
        _rb.AddForce((-_transform.forward + Vector3.up) * _knockbackForce, ForceMode.Impulse);
        yield return new WaitForSeconds(.5f);
        _gettingHit = false;
        _rb.velocity = Vector3.zero;
    }

    #region Stamina
    void AddStamina()
    {
        if (_currentStamina >= _maxStamina) return;

        _staminaTimer -= Time.deltaTime * 2;

        if (_staminaTimer <= 0 && _currentStamina < _maxStamina)
        {
            _currentStamina += Time.deltaTime;
            updateStamina(_currentStamina / _maxStamina);
        }
    }
    void SubstactStamina(float amount)
    {
        _staminaTimer = _timeToAddStamina;
        _currentStamina -= amount;
        if (_currentStamina <= 0) _currentStamina = 0;
        updateStamina(_currentStamina / _maxStamina);
    }
    #endregion
}
