using UnityEngine;
public class Spear : MonoBehaviour
{
    [SerializeField] float damage;
    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
            damageable.TakeDamage(damage);

        PlayerModel.pogoAction();
    }
}
