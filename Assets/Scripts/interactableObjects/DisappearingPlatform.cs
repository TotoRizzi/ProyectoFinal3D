using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : Platform
{
    [SerializeField] float _timeToDestroy = 1;
    [SerializeField] float _timeToAppear = 1;
    float _currentTimeToDestroy = 0;
    float _currentTimeToAppear = 0;

    bool _isInCoroutine = false;

    [SerializeField] GameObject _myPlatform;
    Collider _myCollider;

    private void Start()
    {
        _myCollider = GetComponent<BoxCollider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisione");
        if (!_isInCoroutine)
        {
            StartCoroutine(DisappearPlatform());
            _isInCoroutine = true;
        }
    }
    IEnumerator DisappearPlatform()
    {
        while(_currentTimeToDestroy < _timeToDestroy)
        {
            _currentTimeToDestroy += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _currentTimeToDestroy = 0;
        _myCollider.enabled = false;
        _myPlatform.SetActive(false);
        StartCoroutine(AppearPlatform());
        
        yield return null;
    }
    IEnumerator AppearPlatform()
    {
        while (_currentTimeToAppear < _timeToAppear)
        {
            _currentTimeToAppear += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _currentTimeToAppear = 0;
         _isInCoroutine = false;
        _myPlatform.SetActive(true);
        _myCollider.enabled = true;
        
        yield return null;
    }
}
