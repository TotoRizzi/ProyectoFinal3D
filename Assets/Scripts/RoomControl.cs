using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    CameraController _camera;
    Transform _child;
    private void Start()
    {
        _camera = FindObjectOfType<CameraController>();
        _child = transform.GetChild(0);
        _child.GetComponent<BoxCollider>().size = GetComponent<BoxCollider>().size;
        _child.GetComponent<BoxCollider>().center = GetComponent<BoxCollider>().center;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _child.gameObject.SetActive(true);
            _camera.currentRoom = _child;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _camera.currentRoom = _child;
            _child.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
            _child.gameObject.SetActive(false);
    }
}
