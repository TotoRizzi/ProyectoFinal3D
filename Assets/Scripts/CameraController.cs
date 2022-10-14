using System.Collections;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] float _smooth;
    [SerializeField] Vector3 _offset;

    Player _player;
    Vector3 _groundPoint;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _player.getDamage += () => StartCoroutine(Shake(.1f, .1f));
    }
    private void LateUpdate()
    {
        Vector3 targetPos = _player.transform.position + _offset;
        Debug.Log(Physics.CheckSphere(_player.transform.position, .1f, GameManager.instance.GroundLayer));
        float clampY = Mathf.Clamp(targetPos.y + (_groundPoint.y - targetPos.y) * .9f, targetPos.y - _offset.y * 1.5f, targetPos.y + _offset.y * 1.5f);
        targetPos.y = Physics.CheckSphere(_player.transform.position, .1f, GameManager.instance.GroundLayer) ? targetPos.y : clampY;
        transform.position = Vector3.Lerp(transform.position, targetPos, _smooth * Time.deltaTime);
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
