using UnityEngine;
public class Spear : MonoBehaviour
{
    [SerializeField] float _damage;

    float _speed;
    bool _throwing;
    Vector3 _dir;
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
        if (_throwing && collision != null)
        {
            Debug.Log("me rompo");
            Destroy(gameObject);
        }
    }
    #region BUILDER
    public Spear OnThrow()
    {
        _throwing = true;
        return this;
    }
    public Spear SetPosition(Transform t)
    {
        transform.position = t.position;
        return this;
    }
    public Spear SetSpeed(float speed)
    {
        _speed = speed;
        return this;
    }
    public Spear SetDirection(Vector3 dir)
    {
        _dir = dir;
        transform.forward = dir;
        return this;
    }
    public Spear SetCollider()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.center = new Vector3(0, 0, 2.25f);
        return this;
    }
    #endregion
}
