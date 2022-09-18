using System;
using UnityEngine;
public class Spear : MonoBehaviour
{
    [SerializeField] float _damage;
    [SerializeField] float _speed;
    [SerializeField] ParticleSystem _hitSpearPS;
    public Vector3 hitPoint { get; private set; }

    Vector3 _dir;

    public event Action collisionWithEnemy;
    private void Update()
    {
        transform.position += _dir * _speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
            damageable.TakeDamage(_damage);

        //PlayerModel.pogoAction();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            ContactPoint point = collision.contacts[0];
            hitPoint = point.point;
            collisionWithEnemy();
        }

        PlayHitPS();
        Destroy(gameObject);
    }

    void PlayHitPS()
    {
        var hitPS = Instantiate(_hitSpearPS);
        hitPS.transform.SetPositionAndRotation(hitPoint, Quaternion.identity);
        hitPS.Play();
        Destroy(hitPS.gameObject, hitPS.main.duration);
    }

    #region BUILDER
    public Spear SetPosition(Transform t)
    {
        transform.position = t.position;
        return this;
    }
    public Spear SetDirection(Vector3 dir)
    {
        _dir = dir;
        transform.forward = dir;
        return this;
    }
    public Spear SetColliderAndRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.center = new Vector3(0, 0, 2.25f);
        return this;
    }
    #endregion
}
