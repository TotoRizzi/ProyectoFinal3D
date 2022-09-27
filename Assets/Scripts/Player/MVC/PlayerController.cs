using UnityEngine;
public class PlayerController : IController
{
    PlayerModel _playerModel;
    Player _player;

    float _xAxis;
    float _yAxis;

    public PlayerController(PlayerModel playerModel, Player player)
    {
        _playerModel = playerModel;
        _player = player;

        PlayerModel.pogoAction += Pogo;
    }
    public void OnUpdate()
    {
        if (PausedMenu._gameIsPaused) return;

        _xAxis = Input.GetAxisRaw("Horizontal");
        _yAxis = Input.GetAxisRaw("Vertical");

        _playerModel.OnUpdate(_xAxis, _yAxis);

        if (Input.GetKeyDown(KeyCode.Space))
            _playerModel.OnJumpDown();

        if (Input.GetKeyUp(KeyCode.Space))
            _playerModel.OnJumpUp();

        if (Input.GetKeyDown(KeyCode.LeftShift)) _player.StartCoroutine(_playerModel.Dash(_xAxis, _yAxis));

        if (Input.GetKeyDown(KeyCode.J)) _player.StartCoroutine(_playerModel.Attack());

        if (Input.GetKeyDown(KeyCode.K)) _player.StartCoroutine(_playerModel.Throw());

        //_playerModel.pogoAnimation(Input.GetMouseButton(1) && !_playerModel.inGrounded && _yAxis < 0, _xAxis);
    }
    public void OnFixedUpdate()
    {
        _playerModel.OnFixedUpdate();
    }
    void Pogo()
    {
        _playerModel.Pogo(_yAxis);
    }
}
