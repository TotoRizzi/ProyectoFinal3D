using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Player _player;
    public Player Player { get { return _player; } }

    public LayerMask wallLayer { get { return wallLayer; } }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        Debug.Log(_player);
    }

    public Vector3 GetDistanceToPlayer(Transform transform)
    {
        return _player.transform.position - transform.position;
    }
}
