using UnityEngine;
public class BoomerangSpear : Spear
{
    [SerializeField] float _speed;

    Vector3 _dir;
    Transform _initialPos;
    public bool backing { get; private set; }

    public event System.Action collisionWithEnemy;
    void Update()
    {
        if(backing)
            _dir = _initialPos.position;

        transform.position = Vector3.MoveTowards(transform.position, _dir, _speed * Time.deltaTime);
        transform.forward = _dir - transform.position;

        if (Vector3.Distance(transform.position, _dir) < .5f && !backing)
            backing = true;

        if (Vector3.Distance(transform.position, _initialPos.transform.position) < 0.1f && backing)
            Destroy(gameObject);
    }

    //void PlayHitPS()
    //{
    //    var hitPS = Instantiate(_hitSpearPS);
    //    hitPS.transform.SetPositionAndRotation(hitPoint, Quaternion.identity);
    //    hitPS.Play();
    //    Destroy(hitPS.gameObject, hitPS.main.duration);
    //}

    #region BUILDER
    public BoomerangSpear SetPosition(Transform t)
    {
        transform.position = t.position;
        _initialPos = t;
        return this;
    }
    public BoomerangSpear SetDirection(Vector3 dir)
    {
        _dir = dir;
        return this;
    }

    #endregion
}
