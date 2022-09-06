using UnityEngine;
public class Spear : MonoBehaviour
{
    [SerializeField] float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.TakeDamage(damage);
    }
}
