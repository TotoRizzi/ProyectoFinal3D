using System.Collections;
using UnityEngine;
public class PlayerModel
{
    Vector3 _movementInput;
    bool _isJumping;
    bool _canDoubleJump;
    bool _dashing;
    bool _canDash = true;
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

    //bool _reverse => _movementInput.z != 0 && Mathf.Sign(_movementInput.z) != Mathf.Sign(_lookAt.x) && _inGrounded;
    bool _inGrounded => Physics.CheckSphere(_transform.position, 0.1f, _groundLayer);
    bool _canJump => _bufferTimer > 0 && _coyotaTimer > 0 && !_isJumping;
    public PlayerModel(Transform transform, Rigidbody rb, LayerMask groundLayer, float frictionAmount, float movementSpeed, float acceleration, float decceleration,
        float velPower, float jumpCutMultiplier, float jumpForce, float dashForce, float dashTime, float dashCoolDown, float jumpBufferLength,
        float jumpCoyotaTime, float gravityScale, float fallGravityMultiplier)
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
    }
    public void OnUpdate(float xAxis)
    {
        _movementInput = new Vector3(0, 0, xAxis);

        _bufferTimer -= Time.deltaTime;

        if (_canJump)
        {
            Jump();
            _bufferTimer = 0;
        }

        if (_inGrounded || _canDoubleJump)
            _coyotaTimer = _jumpCoyotaTime;
        else
            _coyotaTimer -= Time.deltaTime;

        #region JumpChecks
        if (_isJumping && _rb.velocity.y < 0)
            _isJumping = false;
        #endregion

        #region JumpGravity
        if (_rb.velocity.y < 0)
            _rb.AddForce(Vector3.down * (_gravityScale * _fallGravityMultiplier));
        else
            _rb.AddForce(Vector3.down * _gravityScale);
        #endregion
    }
    public void OnFixedUpdate()
    {
        //_mousePos = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z));
        //_lookAt = _mousePos - _transform.position;

        //Flipeo el sprite del player teniendo en cuenta la posicion del mouse
        if (_movementInput.z != 0)
            _transform.rotation = _movementInput.z <= 0 ? Quaternion.Euler(0, -90, 0) : Quaternion.Euler(0, 90, 0);

        Friction();
        Run();
    }
    void Friction()
    {
        if (Mathf.Abs(_movementInput.z) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(_frictionAmount));

            amount *= Mathf.Sign(_rb.velocity.x);
            _rb.AddForce(Vector3.right * -amount, ForceMode.Impulse);
        }
    }
    void Run()
    {
        if (_dashing) return;

        //float targetSpeed = _reverse ? _movementInput.z * _movementSpeed * .5f : _movementInput.z * _movementSpeed;
        float targetSpeed = _movementInput.z * _movementSpeed;

        float speedDif = targetSpeed - _rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velPower) * Mathf.Sign(speedDif);

        _rb.AddForce(movement * Vector3.right);
    }
    void Jump()
    {
        _canDoubleJump = _inGrounded;
        _rb.AddForce(Vector3.up * (_jumpForce - _rb.velocity.y), ForceMode.Impulse);

        _isJumping = true;
    }
    public void OnJumpUp()
    {
        if (_rb.velocity.y > 0 && _isJumping)
            _rb.AddForce(Vector3.down * _rb.velocity.y * (1 - _jumpCutMultiplier), ForceMode.Impulse);
    }
    public void OnJumpDown()
    {
        _bufferTimer = _jumpBufferLength;
    }
    public IEnumerator Dash(float xAxis, float yAxis)
    {
        if (_canDash)
        {
            _dashing = true;
            _canDash = false;
            _rb.useGravity = false;
            _rb.AddForce(new Vector3(xAxis, yAxis, 0).normalized * _dashForce, ForceMode.Impulse);
            yield return new WaitForSeconds(_dashTime);
            _dashing = false;
            _rb.useGravity = true;
            OnJumpUp();
            yield return new WaitForSeconds(_dashCoolDown);
            _canDash = true;
        }
    }
}
