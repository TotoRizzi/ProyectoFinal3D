using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    Transform _target;
    [SerializeField] float cameraSpeed;
    [SerializeField] Vector3 offset;

    private void Start()
    {
        _target = GameObject.Find("CameraLookAt").transform;
    }
    private void LateUpdate()
    {
        Vector3 desiredPosition = _target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
