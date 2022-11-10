using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] float _timeToDestroy = 1;
    float _currentTimeToDestroy;

    void Update()
    {
        _currentTimeToDestroy += Time.deltaTime;

        if (_currentTimeToDestroy >= _timeToDestroy)
        {
            ResetParticle();
            ReturnToFactory();
        }
    }

    #region Factory

    public virtual void ReturnToFactory() { }
    private void ResetParticle()
    {
        _currentTimeToDestroy = 0;
    }
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
