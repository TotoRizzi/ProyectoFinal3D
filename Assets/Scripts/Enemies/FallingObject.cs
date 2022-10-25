using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] Vector3 _direction;
    [SerializeField] float _damage;
    [SerializeField] float _speed = 8;
    [SerializeField] float _timeToDestroy;
    float _currentTimeToDestroy;

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;

        _currentTimeToDestroy += Time.deltaTime;
        if (_currentTimeToDestroy >= _timeToDestroy) ReturnToFactory();
    }

    private void OnTriggerEnter(Collider other)
    {
        var dealDamage = other.GetComponent<IDamageable>();

        if (dealDamage != null) dealDamage.TakeDamage(_damage);
        Destroy(gameObject);
    }
    #region Factory

    private void Reset()
    {
        _currentTimeToDestroy = 0;
    }
    public virtual void ReturnToFactory()
    {
        Reset();
        FRY_FallingRock.Instance.ReturnObject(this);
    }

    public static void TurnOn(FallingObject b)
    {
        b.gameObject.SetActive(true);  
    }

    public static void TurnOff(FallingObject b)
    {
        b.gameObject.SetActive(false);
    }

    public FallingObject SetPosition(Vector3 pos)
    {
        transform.position = pos;
        return this;
    }
    #endregion
}
