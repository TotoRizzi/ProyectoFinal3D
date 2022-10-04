using UnityEngine;
public class GameCamera : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float cameraSpeed;
    [SerializeField] Vector3 offset;

    private void LateUpdate()
    {
        Vector3 desiredPosition = _target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
