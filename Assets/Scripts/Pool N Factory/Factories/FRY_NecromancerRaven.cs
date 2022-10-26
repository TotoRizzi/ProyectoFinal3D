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


    public NecromancerRaven necromancerRavenPrefab;
    public int objectStock = 2;

    public ObjectPool<NecromancerRaven> pool;


    void Start()
    {
        _instance = this;
        pool = new ObjectPool<NecromancerRaven>(ObjectCreator, NecromancerRaven.TurnOn, NecromancerRaven.TurnOff, objectStock);
    }

    public NecromancerRaven ObjectCreator()
    {
        return Instantiate(necromancerRavenPrefab);
    }

    public void ReturnObject(NecromancerRaven b)
    {
        pool.ReturnObject(b);
    }
}
