using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] float _smooth;
    [SerializeField] float _zCameraMuiltiplier;
    [SerializeField] Vector3 _offset;

    Transform _player;
    float _offsetY;
    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        _offsetY = _offset.y;
    }
    private void LateUpdate()
    {
        Vector3 targetPos = _player.position + _offset;
        float addZ = targetPos.y - transform.position.y > 0 ? targetPos.y - transform.position.y : 0;
        targetPos.z -= addZ * _zCameraMuiltiplier;
        transform.position = Vector3.Lerp(transform.position, targetPos, _smooth * Time.deltaTime);
    }
}
