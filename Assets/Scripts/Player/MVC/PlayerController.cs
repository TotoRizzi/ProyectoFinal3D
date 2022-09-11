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
    }
    public void OnUpdate()
    {
        _xAxis = Input.GetAxis("Horizontal");
        _yAxis = Input.GetAxis("Vertical");

        _playerModel.OnUpdate(_xAxis);

        if (Input.GetKeyDown(KeyCode.Space))
            _playerModel.OnJumpDown();

        if (Input.GetKeyUp(KeyCode.Space))
            _playerModel.OnJumpUp();

        if (Input.GetKeyDown(KeyCode.LeftShift)) _player.StartCoroutine(_playerModel.Dash(_xAxis, _yAxis));

        if (Input.GetMouseButtonDown(0)) _player.StartCoroutine(_playerModel.Attack());

        if (Input.GetMouseButtonDown(1)) _player.StartCoroutine(_playerModel.Pogo(_xAxis, _yAxis));
    }
    public void OnFixedUpdate()
    {
        _playerModel.OnFixedUpdate();
    }
}
