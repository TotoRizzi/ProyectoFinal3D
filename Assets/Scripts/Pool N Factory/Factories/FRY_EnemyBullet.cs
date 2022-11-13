using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRY_EnemyBullet : MonoBehaviour
{
    public static FRY_EnemyBullet Instance
    {
        get
        {
            return _instance;
        }
    }
    static FRY_EnemyBullet _instance;


    public EnemyBullet enemyBulletPrefab;
    public int sctock = 2;

    public ObjectPool<EnemyBullet> pool;


    void Start()
    {
        _instance = this;
        pool = new ObjectPool<EnemyBullet>(ObjectCreator, EnemyBullet.TurnOn, EnemyBullet.TurnOff, sctock);
    }

    public EnemyBullet ObjectCreator()
    {
        return Instantiate(enemyBulletPrefab);
    }

    public void ReturnObject(EnemyBullet b)
    {
        pool.ReturnObject(b);
    }
}
