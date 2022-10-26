using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRY_FallingRock : MonoBehaviour
{
    public static FRY_FallingRock Instance
    {
        get
        {
            return _instance;
        }
    }
    static FRY_FallingRock _instance;


    public FallingObject fallingRockPrefab;
    public int objectStock = 2;

    public ObjectPool<FallingObject> pool;


    void Start()
    {
        _instance = this;
        pool = new ObjectPool<FallingObject>(ObjectCreator, FallingObject.TurnOn, FallingObject.TurnOff, objectStock);
    }

    public FallingObject ObjectCreator()
    {
        return Instantiate(fallingRockPrefab);
    }

    public void ReturnObject(FallingObject b)
    {
        pool.ReturnObject(b);
    }
}
