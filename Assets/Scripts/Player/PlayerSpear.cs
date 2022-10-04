using UnityEngine;
public class PlayerSpear : Spear
{
    public event System.Action pogoAction;
    public bool canUseSpear { get; set; } = true;
    public void ActiveSpear(BoomerangSpear bs)
    {
        gameObject.SetActive(true);
        canUseSpear = true;
        Destroy(bs.gameObject);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<Enemy>();

        if (damageable != null)
        {
            pogoAction();
            damageable.TakeDamage(_damage);
        }
    }
}
