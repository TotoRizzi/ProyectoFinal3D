using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRY_NecromancerRaven : MonoBehaviour
{
    public static FRY_NecromancerRaven Instance
    {
        get
        {
            return _instance;
        }
    }
    static FRY_NecromancerRaven _instance;


    public NecromancerRavenEnemy necromancerRavenPrefab;
    public int objectStock = 2;

    public ObjectPool<NecromancerRavenEnemy> pool;


    void Start()
    {
        _instance = this;
        pool = new ObjectPool<NecromancerRavenEnemy>(ObjectCreator, NecromancerRavenEnemy.TurnOn, NecromancerRavenEnemy.TurnOff, objectStock);
    }

    public NecromancerRavenEnemy ObjectCreator()
    {
        return Instantiate(necromancerRavenPrefab);
    }

    public void ReturnObject(NecromancerRavenEnemy b)
    {
        pool.ReturnObject(b);
    }
}
