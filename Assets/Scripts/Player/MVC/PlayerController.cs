using UnityEngine;
public class PlayerController : IController
{
    PlayerModel _playerModel;
    Player _player;
    InputManager _inputManager;

    float _xAxis;
    float _yAxis;
    float f;
    bool _startDash;
    public PlayerController(PlayerModel playerModel, Player player, InputManager inputManager)
    {
        _playerModel = playerModel;
        _player = player;
        _inputManager = inputManager;

        PlayerModel.pogoAction += Pogo;
    }
    public void OnUpdate()
    {
        if (PausedMenu._gameIsPaused) return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            f = Mathf.Clamp(f, -1, 1);
        }
        //_xAxis = Input.GetAxisRaw("Horizontal");
        //_yAxis = Input.GetAxisRaw("Vertical");
        _xAxis = _inputManager.GetAxisRaw("Horizontal");
        _yAxis = _inputManager.GetAxisRaw("Vertical");

        _playerModel.OnUpdate(_xAxis, _yAxis);

        if (_inputManager.GetButtonDown("Jump"))
            _playerModel.OnJumpDown();

        if (_inputManager.GetButtonUp("Jump"))
            _playerModel.onJumpUp = true;

        //if (_inputManager.GetButtonDown("Dash")) _startDash = true;
        //if (_inputManager.GetButtonUp("Dash")) _startDash = false;

        if (_inputManager.GetButtonDown("Attack")) _playerModel.Attack(_yAxis);

        if (_inputManager.GetButtonDown("Throw")) _player.StartCoroutine(_playerModel.Throw());
    }
    public void OnFixedUpdate()
    {
        _playerModel.OnFixedUpdate();

        if(_startDash)
            _player.StartCoroutine(_playerModel.Dash(_xAxis, _yAxis));
    }
    void Pogo()
    {
        _playerModel.Pogo(_yAxis);
    }
}
