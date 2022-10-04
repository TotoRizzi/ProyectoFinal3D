using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : Platform
{
    [SerializeField] float _timeToDestroy = 1;
    float _currentTimeToDestroy = 0;

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(DisappearPlatform());
    }
    IEnumerator DisappearPlatform()
    {
        while(_currentTimeToDestroy < _timeToDestroy)
        {
            _currentTimeToDestroy += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
        yield return null;
    }
}
