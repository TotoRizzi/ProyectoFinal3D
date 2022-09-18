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

        //PlayerModel.pogoAction += StartPogo;
    }
    public void OnUpdate()
    {
        if (PausedMenu._gameIsPaused) return;

        _xAxis = Input.GetAxisRaw("Horizontal");
        _yAxis = Input.GetAxisRaw("Vertical");

        _playerModel.OnUpdate(_xAxis);

        if (Input.GetKeyDown(KeyCode.Z))
            _playerModel.OnJumpDown();

        if (Input.GetKeyUp(KeyCode.Z))
            _playerModel.OnJumpUp();

        if (Input.GetKeyDown(KeyCode.C)) _player.StartCoroutine(_playerModel.Dash(_xAxis, _yAxis));

        if (Input.GetKeyDown(KeyCode.X)) _player.StartCoroutine(_playerModel.Attack());

        if (Input.GetKeyDown(KeyCode.A)) _player.StartCoroutine(_playerModel.Throw());

        //_playerModel.pogoAnimation(Input.GetMouseButton(1) && !_playerModel.inGrounded && _yAxis < 0, _xAxis);
    }
    public void OnFixedUpdate()
    {
        _playerModel.OnFixedUpdate();
    }
    void StartPogo()
    {
        _player.StartCoroutine(_playerModel.Pogo(_xAxis, _yAxis));
    }
}
