using UnityEngine;
public class PlayerController : IController
{
    PlayerModel _playerModel;
    Player _player;

    float xAxis;
    float yAxis;
    public PlayerController(PlayerModel playerModel, Player player)
    {
        _playerModel = playerModel;
        _player = player;
    }
    public void OnUpdate()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        _playerModel.OnUpdate(xAxis);

        if (Input.GetKeyDown(KeyCode.Space))
            _playerModel.OnJumpDown();

        if (Input.GetKeyUp(KeyCode.Space))
            _playerModel.OnJumpUp();

        if (Input.GetKeyDown(KeyCode.LeftShift)) _player.StartCoroutine(_playerModel.Dash(xAxis, yAxis));

        if (Input.GetMouseButtonDown(1)) _playerModel.Pogo(xAxis, yAxis);
    }
    public void OnFixedUpdate()
    {
        _playerModel.OnFixedUpdate();
    }
}
