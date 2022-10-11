using UnityEngine;
public class Spear : MonoBehaviour
{
    [SerializeField] protected float _damage;
    [SerializeField] protected ParticleSystem _blood;
    protected virtual void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<Enemy>();

        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
            _blood.Play();
        }
    }
}
