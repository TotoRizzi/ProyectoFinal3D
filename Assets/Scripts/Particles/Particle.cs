using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] float _timeToDestroy;

    private void Start()
    {
        StartCoroutine(DestroyTimer());
    }
    IEnumerator DestroyTimer()
    {
        while (_timeToDestroy > 0)
        {
            _timeToDestroy -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
        yield return null;
    }
}
