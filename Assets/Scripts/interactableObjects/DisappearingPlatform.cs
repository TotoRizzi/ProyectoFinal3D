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
    [SerializeField] Animator myAnim;
    [SerializeField] GameObject _myPlatform;
    Collider _myCollider;

    private void Start()
    {   
        //myAnim = GetComponentInChildren<Animator>();
        _myCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!_isInCoroutine && player)
        {
            StartCoroutine(DisappearPlatform());
            _isInCoroutine = true;
        }
    }
    IEnumerator DisappearPlatform()
    {
        FRY_DisappearingPlatformParticle.Instance.pool.GetObject().SetPosition(transform.position);
        myAnim.SetBool("Shake",true);
        AudioManager.Instance.PlaySFX("FallingPlat");
        while (_currentTimeToDestroy < _timeToDestroy)
        {
            _currentTimeToDestroy += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _currentTimeToDestroy = 0;
        _myCollider.enabled = false;
        StartCoroutine(AppearPlatform());
        _myPlatform.SetActive(false);
        
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
        myAnim.SetBool("Shake",false);
        
        yield return null;
    }
}
