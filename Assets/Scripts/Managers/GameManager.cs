using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Player _player;
    [SerializeField] LayerMask _wallLayer, _groundLayer, _playerLayer, _enemyLayer;

    #region Getters

    public Player Player { get { return _player; } }
    public LayerMask WallLayer { get { return _wallLayer; } }
    public LayerMask GroundLayer { get { return _groundLayer; } }
    public LayerMask PlayerLayer { get { return _playerLayer; } }
    public LayerMask EnemyLayer { get { return _enemyLayer; } }


    #endregion

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public Vector3 GetDirectionToPlayer(Transform transform)
    {
        return (_player.transform.position + transform.up) - transform.position;
    }
}
