using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRY_NecromancerTpParicle : MonoBehaviour
{
    public static FRY_NecromancerTpParicle Instance
    {
        get
        {
            return _instance;
        }
    }
    static FRY_NecromancerTpParicle _instance;


    public PT_NecromancerTp necromancerTpPrefab;
    public int particleStock = 2;

    public ObjectPool<PT_NecromancerTp> pool;


    void Start()
    {
        _instance = this;
        pool = new ObjectPool<PT_NecromancerTp>(ObjectCreator, PT_NecromancerTp.TurnOn, PT_NecromancerTp.TurnOff, particleStock);
    }

    public PT_NecromancerTp ObjectCreator()
    {
        return Instantiate(necromancerTpPrefab);
    }

    public void ReturnObject(PT_NecromancerTp b)
    {
        pool.ReturnObject(b);
    }
}
