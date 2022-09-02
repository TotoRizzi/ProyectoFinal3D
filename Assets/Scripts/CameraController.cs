using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float _smooth;

    Transform _player;
    Vector3 _offset = new Vector3(0, 0, -10);
    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
    }
    private void LateUpdate()
    {
        Vector3 targetPos = _player.position + Vector3.up + _offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, _smooth * Time.deltaTime);
    }
}
