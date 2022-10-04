using UnityEngine;
public class BoomerangSpear : Spear
{
    [SerializeField] float _speed;
    [SerializeField] float _maxForce;

    Vector3 _dir;
    Vector3 _velocity;
    Transform _initialPos;
    bool _backing;
    public PlayerSpear playerSpear { get; set; }
    void Update()
    {
        if (_backing)
            _dir = new Vector3(_initialPos.position.x, _initialPos.position.y, GameManager.instance.Player.transform.position.z);

        Seek();
        transform.position += _velocity * Time.deltaTime;
        transform.forward = _velocity;

        if (Vector3.Distance(transform.position, _dir) < .1f && !_backing ||
            Physics.Raycast(transform.position, transform.forward, 1f, GameManager.instance.GroundLayer) && !_backing)
        {
            _backing = true;
            _maxForce *= .25f;
        }

        if (Vector3.Distance(transform.position, _initialPos.position) < 1f && _backing)
            playerSpear.ActiveSpear(this);
    }
    void Seek()
    {
        Vector3 desired = _dir - transform.position;
        desired.Normalize();
        desired *= _speed;

        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);
        ApplyForce(steering);
    }
    void ApplyForce(Vector3 force)
    {
        _velocity += force;
        _velocity = Vector3.ClampMagnitude(_velocity, _speed);
    }

    #region BUILDER
    public BoomerangSpear SetPosition(Transform t)
    {
        transform.position = new Vector3(t.position.x, t.position.y, GameManager.instance.Player.transform.position.z);
        _initialPos = t;
        return this;
    }
    public BoomerangSpear SetDirection(Vector3 dir)
    {
        _dir = new Vector3(dir.x, dir.y, GameManager.instance.Player.transform.position.z);
        return this;
    }

    #endregion
}
