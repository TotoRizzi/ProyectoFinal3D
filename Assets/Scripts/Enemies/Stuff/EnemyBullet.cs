using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float _timeToDestroy = 2;
    float _currentTimeToDestroy;

    [SerializeField] float _speed;
    Vector3 _dir;
    float _dmg;

    private void Update()
    {
        transform.position += _dir * _speed * Time.deltaTime;

        _currentTimeToDestroy += Time.deltaTime;
        if (_currentTimeToDestroy >= _timeToDestroy) ReturnToFactory();
    }

    public EnemyBullet SetDmg(float dmg)
    {
        _dmg = dmg;
        return this;
    }
    public EnemyBullet SetDirection(Vector3 dir)
    {
        _dir = dir;
        return this;
    }
    
    public EnemyBullet SetPosition(Vector3 pos)
    {
        transform.position = pos;
        return this;
    }
    private void Reset()
    {
        _currentTimeToDestroy = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IDamageable>().TakeDamage(_dmg);

        ReturnToFactory();        
    }
    public virtual void ReturnToFactory() 
    {
        Reset();
        FRY_EnemyBullet.Instance.ReturnObject(this);
    }

    public static void TurnOn(EnemyBullet b)
    {
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(EnemyBullet b)
    {
        b.gameObject.SetActive(false);
    }
}
