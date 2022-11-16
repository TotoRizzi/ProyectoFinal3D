using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RavensUISignal : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;

    SimpleRavenEnemy _raven;
    RectTransform _rectTransform;
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCamera = GameManager.instance.Camera;
    }
    private void Update()
    {
        Vector3 ravenPos = _mainCamera.WorldToScreenPoint(_raven.transform.position);
        Vector3 newPosition = new Vector3(ravenPos.x, ravenPos.y, 0);

        if(ravenPos.z < 0)
        {
            ravenPos.x = -ravenPos.x;
            ravenPos.y = -ravenPos.y;
        }

        ravenPos.x = Mathf.Clamp(ravenPos.x, _rectTransform.rect.width / 2, Screen.width - _rectTransform.rect.width / 2);
        ravenPos.y = Mathf.Clamp(ravenPos.y, _rectTransform.rect.height / 2, Screen.height - _rectTransform.rect.height / 2);

        _rectTransform.right = (newPosition - ravenPos).normalized;
        _rectTransform.position = ravenPos;
    }
    public RavensUISignal SetRaven(SimpleRavenEnemy raven)
    {
        _raven = raven;
        return this;
    }
    public static void TurnOn(RavensUISignal r)
    {
        r.gameObject.SetActive(true);
    }
    public static void TurnOff(RavensUISignal r)
    {
        r.gameObject.SetActive(false);
    }
    public void ReturnToFactory()
    {
        FRY_RavensUISignal.Instance.ReturnObject(this);
    }
}
