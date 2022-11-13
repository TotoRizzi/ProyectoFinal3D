using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovingTorret : WaypointGroundEnemy
{
    [SerializeField] float _bulletDmg;
    [SerializeField] Transform _myArm;
    [SerializeField] float attackSpeed = 1;
    float _currentAttackSpeed;

    protected override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        if(CanSeePlayer()) ArmLookAtPlayer();

        _currentAttackSpeed += Time.deltaTime;
        if (_currentAttackSpeed > attackSpeed)
        {
            _currentAttackSpeed = 0;
            Shoot();
        }
    }
    void Shoot()
    {
        FRY_EnemyBullet.Instance.pool.GetObject().SetDirection(_myArm.right)
                                                 .SetDmg(_bulletDmg);

    }
    private void ArmLookAtPlayer()
    {
        Vector3 dirToLookAt = GetDirToPlayer();
        float angle = Mathf.Atan2(dirToLookAt.y, dirToLookAt.x) * Mathf.Rad2Deg;

        _myArm.transform.eulerAngles = new Vector3(0, 0, angle);
    }
    Vector3 GetDirToPlayer()
    {
        var dir = GameManager.instance.Player.transform.position - transform.position;
        dir.z = 0;
        dir.Normalize();

        return dir;
    }
}
