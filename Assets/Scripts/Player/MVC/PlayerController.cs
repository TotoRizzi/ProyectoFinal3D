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

        _playerModel.playerSpear.pogoAction += () => _playerModel.Pogo(_yAxis);
    }
    public void OnUpdate()
    {
        if (PausedMenu._gameIsPaused) return;

        _xAxis = _inputManager.GetAxisRaw("Horizontal");
        _yAxis = _inputManager.GetAxisRaw("Vertical");

        _playerModel.OnUpdate();

        if (_inputManager.GetButtonDown("Jump"))
            _playerModel.OnJumpDown();

        if (_inputManager.GetButtonUp("Jump"))
            _playerModel.onJumpUp = true;

        //if (_inputManager.GetButtonDown("Dash")) _startDash = true;
        //if (_inputManager.GetButtonUp("Dash")) _startDash = false;

        if (_inputManager.GetButtonDown("Attack")) _playerModel.Attack(_yAxis);

        if (_inputManager.GetButtonDown("Throw"))
        {
            if (_playerModel.playerSpear.canUseSpear)
                _playerModel.Throw();
            else
                _player.MoveToSpear();
        }
    }
    public void OnFixedUpdate()
    {
        _playerModel.OnFixedUpdate(_xAxis);

        if (_startDash)
            _player.StartCoroutine(_playerModel.Dash(_xAxis, _yAxis));
    }
}
