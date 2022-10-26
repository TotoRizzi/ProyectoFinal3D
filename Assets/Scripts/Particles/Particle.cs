using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] float _timeToDestroy = 2;

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
        ReturnToFactory();
        yield return null;
    }
    #region Factory

    public virtual void ReturnToFactory() { }

    public static void TurnOn(Particle b)
    {
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Particle b)
    {
        b.gameObject.SetActive(false);
    }
    #endregion
}
