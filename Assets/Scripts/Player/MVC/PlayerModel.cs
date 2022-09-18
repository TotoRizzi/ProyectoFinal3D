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
    bool _canPog;
    bool _attacking;
    bool _jumpFalling;
    bool _throwing;
    float _coyotaTimer;
    float _bufferTimer;

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
    float _timeToAttack;
    float _timeToThrow;
    Spear _playerSpear;
    TrailRenderer _tr;
    public bool inGrounded => Physics.CheckSphere(_transform.position, 0.1f, GameManager.instance.GroundLayer);
    bool _canJump => _bufferTimer > 0 && _coyotaTimer > 0 && !_isJumping;

    public Action<float> runAction;
    public Action<bool> jumpAction;
    public Action<bool> fallingAction;
    public Action dashAction;
    public Action attackAction;
    public Action<bool> inGroundedAction;
    //public Action<bool, float> pogoAnimation;
    //public static Action pogoAction;
    //public Action pogoFeedback;
    public Action throwAnimation;
    public Action<float> throwAction;
    public PlayerModel(Transform transform, Rigidbody rb, float groundFriction, float movementSpeed, float acceleration,
        float decceleration, float velPower, float jumpCutMultiplier, float jumpForce, float dashForce, float dashTime, float dashCoolDown,
        float jumpBufferLength, float jumpCoyotaTime, float gravityScale, float fallGravityMultiplier, float pogoForce, float attackCooldown, float timeToThrow,
        Spear playerSpear, TrailRenderer tr)
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
        _timeToAttack = attackCooldown;
        _timeToThrow = timeToThrow;
        _playerSpear = playerSpear;
        _tr = tr;

        //pogoAnimation += CanPog;
    }
    public void OnUpdate(float xAxis)
    {
        _xAxis = xAxis;
        _bufferTimer -= Time.deltaTime;

        if (inGrounded)
        {
            _coyotaTimer = _jumpCoyotaTime;
            _jumpFalling = true;
            _canDoubleJump = false;
            _poging = false;
        }
        else
            _coyotaTimer -= Time.deltaTime;

        Falling(!inGrounded);
        fallingAction(_rb.velocity.y < 0 && !inGrounded);
        inGroundedAction(inGrounded);
    }
    public void OnFixedUpdate()
    {
        //Flipeo el sprite del player teniendo en cuenta la posicion del mouse
        if (_xAxis != 0 && !_attacking)
            _transform.eulerAngles = Vector3.Lerp(Quaternion.Euler(0, 90, 0).eulerAngles, Quaternion.Euler(0, -90, 0).eulerAngles, 0) * _xAxis;

        GroundFriction();
        Run();

        #region JumpChecks
        if (_isJumping && _rb.velocity.y < 0)
            _isJumping = false;
        #endregion

        if (!_canDash && _rb.velocity.magnitude > _dashForce)
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _dashForce);

        if (_poging && _rb.velocity.magnitude > _pogoForce)
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _pogoForce);
    }
    void GroundFriction()
    {
        if (Mathf.Abs(_xAxis) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(_groundFriction));
            amount *= Mathf.Sign(_rb.velocity.x);
            _rb.AddForce(Vector3.right * -amount, ForceMode.Impulse);
        }
    }
    public void Run()
    {
        if (_poging || _dashing || _attacking || _throwing) return;

        if (inGrounded)
            runAction(_xAxis);

        float targetSpeed = _xAxis * _movementSpeed;

        float speedDif = targetSpeed - _rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.5f) ? _acceleration : _decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velPower) * Mathf.Sign(speedDif);

        float finalMovement = inGrounded ? movement : movement * 0.75f;

        _rb.AddForce(finalMovement * Vector3.right);
    }
    void Jump()
    {
        if (_dashing || _poging || _attacking) return;
        _rb.AddForce(Vector3.up * (_jumpForce - _rb.velocity.y), ForceMode.Impulse);
        jumpAction(_canDoubleJump || _jumpFalling);
        _isJumping = true;
        _poging = false;
    }
    public void OnJumpUp()
    {
        if (_rb.velocity.y < 0) return;
        _rb.AddForce(Vector3.down * _rb.velocity.y * (1 - _jumpCutMultiplier), ForceMode.Impulse);
    }
    public void OnJumpDown()
    {
        _bufferTimer = _jumpBufferLength;
        if (!_canDoubleJump)
        {
            if (inGrounded)                     //Primer salto desde el suelo
            {
                _canDoubleJump = true;
                _jumpFalling = false;
            }
            else if (_coyotaTimer > 0)          //Primer salto con coyota time
                _canDoubleJump = true;
            else if (_jumpFalling)              //Salto sin estar en el suelo
            {
                _coyotaTimer = _jumpCoyotaTime;
                _jumpFalling = false;
            }
        }
        else                                    //Doble salto
        {
            _coyotaTimer = _jumpCoyotaTime;
            _canDoubleJump = false;
            _jumpFalling = false;
        }

        if (_canJump)
        {
            Jump();
            _bufferTimer = 0;
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
            _tr.emitting = true;
            yield return new WaitForSeconds(_dashTime);
            _dashing = false;
            _rb.useGravity = true;
            _tr.emitting = false;
            yield return new WaitForSeconds(_dashCoolDown);
            _canDash = true;
        }
    }
    public void CanPog(bool pogoBool, float x)
    {
        if (pogoBool)
            _canPog = true;
        else
            _canPog = false;
    }
    public IEnumerator Pogo(float xAxis, float yAxis)
    {
        Vector3 pogoDirection = new Vector3(xAxis * _pogoForce + _rb.velocity.x / 2, yAxis * _pogoForce + _rb.velocity.y, 0);
        if (_canPog)
        {
            _rb.AddForce(-pogoDirection, ForceMode.Impulse);
            //pogoFeedback();
            _poging = true;
            _isJumping = false;
            yield return new WaitUntil(() => _rb.velocity.y > 0);
            yield return new WaitUntil(() => _rb.velocity.y < 0);
            _poging = false;
        }
    }
    public void Falling(bool falling)
    {
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
            _rb.velocity = Vector3.zero;
            yield return new WaitForSeconds(_timeToAttack);
            _attacking = false;
        }
    }
    public IEnumerator Throw()
    {
        if (!_throwing)
        {
            _throwing = true;
            throwAnimation();
            _rb.velocity = Vector3.zero;
            yield return new WaitForSeconds(_timeToThrow);
            _playerSpear.gameObject.SetActive(true);
            _throwing = false;
        }
    }
}
