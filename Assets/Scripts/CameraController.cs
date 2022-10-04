using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] float _smooth;
    [SerializeField] Vector3 _offset = new Vector3(0, 0, -50);

    Transform _player;
    Vector3 _initialPos;
    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        _initialPos = _player.transform.position;
    }
    private void LateUpdate()
    {
        float addZ = _player.transform.position.y - _initialPos.y;
        Vector3 targetPos = _player.position + Vector3.up + _offset;
        targetPos.z -= addZ / 2;
        transform.position = Vector3.Lerp(transform.position, targetPos, _smooth * Time.deltaTime);
    }
}
