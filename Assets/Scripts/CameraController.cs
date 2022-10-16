using System.Collections;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] float _smooth;
    [SerializeField] float _minX;
    [SerializeField] float _minY;
    [SerializeField] float _maxX;
    [SerializeField] float _maxY;

    public Transform currentRoom;

    Player _player;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _player.getDamage += () => StartCoroutine(Shake(.1f, .1f));
    }
    private void LateUpdate()
    {
        float minX = currentRoom.GetComponent<BoxCollider>().bounds.min.x + _minX;
        float minY = currentRoom.GetComponent<BoxCollider>().bounds.min.y + _minY;
        float maxX = currentRoom.GetComponent<BoxCollider>().bounds.max.x + _maxX;
        float maxY = currentRoom.GetComponent<BoxCollider>().bounds.max.y + _maxY;

        Vector3 clampedPos = new Vector3(Mathf.Clamp(_player.transform.position.x, minX, maxX), Mathf.Clamp(_player.transform.position.y, minY, maxY), transform.position.z);

        transform.position = Vector3.Lerp(transform.position, clampedPos, _smooth * Time.deltaTime);
    }
    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
