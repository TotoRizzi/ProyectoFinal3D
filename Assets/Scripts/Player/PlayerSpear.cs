using UnityEngine;
public class PlayerSpear : Spear
{
    public System.Action pogoAction;
    protected override void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<Enemy>().GetComponent<IDamageable>();

        if (damageable != null)
        {
            pogoAction();
            damageable.TakeDamage(_damage);
        }
    }
}
