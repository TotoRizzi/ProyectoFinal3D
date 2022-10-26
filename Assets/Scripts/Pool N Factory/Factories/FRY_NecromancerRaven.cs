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


    public SimpleRaven fallingRockPrefab;
    public int objectStock = 2;

    public ObjectPool<SimpleRaven> pool;


    void Start()
    {
        _instance = this;
        pool = new ObjectPool<SimpleRaven>(ObjectCreator, SimpleRaven.TurnOn, SimpleRaven.TurnOff, objectStock);
    }

    public SimpleRaven ObjectCreator()
    {
        return Instantiate(fallingRockPrefab);
    }

    public void ReturnObject(SimpleRaven b)
    {
        pool.ReturnObject(b);
    }
}
