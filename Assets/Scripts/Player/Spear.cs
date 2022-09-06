using UnityEngine;
public class Spear : MonoBehaviour
{
    [SerializeField] float damage;
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<IDamageable>();
        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
            damageable.TakeDamage(damage);
    }
}
