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
        if (_currentTimeToDestroy >= _timeToDestroy) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var dealDamage = other.GetComponent<IDamageable>();

        if (dealDamage != null) dealDamage.TakeDamage(_damage);
        Destroy(gameObject);
    }
}
