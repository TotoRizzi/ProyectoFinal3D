using System.Collections;
using UnityEngine;
public class PlayerModel
{
    Camera _mainCamera;
    Vector2 _movementInput;
    Vector2 _lookAt;
    Vector3 _mousePos;
    Vector2 _groundCheckSize = new Vector2(.2f, .2f);
    bool _isJumping;
    bool _canDoubleJump;
    bool _dashing;
    bool _canDash = true;
    float _coyotaTimer;
    float _bufferTimer;

    //Variables Constructor
    Transform _transform;
    Rigidbody2D _rb;
    LayerMask _groundLayer;
    float _frictionAmount;
    float _movementSpeed;
    float _acceleration;
    float _decceleration;
    float _velPower;
    float _jumpCutMultiplier;
    float _jumpForce;
    float _doubleJumpForce;
    float _dashForce;
    float _dashTime;
    float _dashCoolDown;
    float _jumpBufferLength;
    float _jumpCoyotaTime;
    float _gravityScale;
    float _fallGravityMultiplier;

    bool _reverse => _movementInput.x != 0 && Mathf.Sign(_movementInput.x) != Mathf.Sign(_lookAt.x) && _inGrounded;
    bool _inGrounded => Physics2D.OverlapBox(_transform.position, _groundCheckSize, 0, _groundLayer);
    bool _canJump => _bufferTimer > 0 && _coyotaTimer > 0 && !_isJumping;
    public PlayerModel(Transform transform, Rigidbody2D rb, LayerMask groundLayer, float frictionAmount, float movementSpeed, float acceleration, float decceleration,
        float velPower, float jumpCutMultiplier, float jumpForce, float doubleJumpForce, float dashForce, float dashTime, float dashCoolDown, float jumpBufferLength,
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
        _doubleJumpForce = doubleJumpForce;
        _dashForce = dashForce;
        _dashTime = dashTime;
        _dashCoolDown = dashCoolDown;
        _jumpBufferLength = jumpBufferLength;
        _jumpCoyotaTime = jumpCoyotaTime;
        _gravityScale = gravityScale;
        _fallGravityMultiplier = fallGravityMultiplier;

        _mainCamera = Camera.main;
    }
    public void OnUpdate(float xAxis)
    {
        _movementInput = new Vector2(xAxis, 0);

        _bufferTimer -= Time.deltaTime;

        if (_canJump)
        {
            Jump(_jumpForce);
            _bufferTimer = 0;
        }

        if (_inGrounded)
            _coyotaTimer = _jumpCoyotaTime;
        else
            _coyotaTimer -= Time.deltaTime;

        #region JumpChecks
        if (_isJumping && _rb.velocity.y < 0)
            _isJumping = false;
        #endregion

        #region JumpGravity
        if (_rb.velocity.y < 0)
            _rb.gravityScale = _gravityScale * _fallGravityMultiplier;
        else
            _rb.gravityScale = _gravityScale;
        #endregion
    }
    public void OnFixedUpdate()
    {
        _mousePos = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z));
        _lookAt = _mousePos - _transform.position;

        //Flipeo el sprite del player teniendo en cuenta la posicion del mouse
        if (_lookAt.x != 0)
            _transform.rotation = _lookAt.x <= 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);

        Friction();
        Run();
    }
    void Friction()
    {
        if (Mathf.Abs(_movementInput.x) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(_frictionAmount));

            amount *= Mathf.Sign(_rb.velocity.x);
            _rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }
    void Run()
    {
        if (_dashing) return;

        float targetSpeed = _reverse ? _movementInput.x * _movementSpeed * .5f : _movementInput.x * _movementSpeed;

        float speedDif = targetSpeed - _rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velPower) * Mathf.Sign(speedDif);

        _rb.AddForce(movement * Vector2.right);
    }
    void Jump(float jumpForce)
    {
        if (_reverse)
            return;

        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        _isJumping = true;
        _canDoubleJump = true;
    }
    void DoubleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
            Jump(Mathf.Abs(_doubleJumpForce - _rb.velocity.y));

        _canDoubleJump = false;
    }
    public void OnJumpUp()
    {
        if (_rb.velocity.y > 0 && _isJumping)
            _rb.AddForce(Vector2.down * _rb.velocity.y * (1 - _jumpCutMultiplier), ForceMode2D.Impulse);
    }
    public void OnJumpDown()
    {
        _bufferTimer = _jumpBufferLength;
        DoubleJump();
    }
    public IEnumerator Dash(float xAxis, float yAxis)
    {
        if (_canDash)
        {
            _dashing = true;
            _canDash = false;
            float originalGravity = _rb.gravityScale;
            _rb.gravityScale = 0f;
            _rb.AddForce(new Vector2(xAxis, yAxis).normalized * _dashForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(_dashTime);
            _rb.gravityScale = originalGravity;
            _dashing = false;
            OnJumpUp();
            yield return new WaitForSeconds(_dashCoolDown);
            _canDash = true;
        }
    }
}
