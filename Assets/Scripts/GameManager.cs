using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Player _player;

    [SerializeField] LayerMask _wallLayer, _groundLayer;
    #region Getters

    public Player Player { get { return _player; } }
    public LayerMask WallLayer { get { return _wallLayer; } }
    public LayerMask GroundLayer { get { return _groundLayer; } }

    #endregion

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
    }

    public Vector3 GetDistanceToPlayer(Transform transform)
    {
        return _player.transform.position - transform.position;
    }
}
