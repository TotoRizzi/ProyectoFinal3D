using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRY_NecromancerDisappearParticle : MonoBehaviour
{
    public static FRY_NecromancerDisappearParticle Instance
    {
        get
        {
            return _instance;
        }
    }
    static FRY_NecromancerDisappearParticle _instance;

    public PT_NecromancerDisappearParticle particlePrefab;
    public int objectStock = 2;

    public ObjectPool<PT_NecromancerDisappearParticle> pool;

    void Start()
    {
        _instance = this;
        pool = new ObjectPool<PT_NecromancerDisappearParticle>(ObjectCreator, PT_NecromancerDisappearParticle.TurnOn, PT_NecromancerDisappearParticle.TurnOff, objectStock);
    }

    public PT_NecromancerDisappearParticle ObjectCreator()
    {
        return Instantiate(particlePrefab);
    }

    public void ReturnObject(PT_NecromancerDisappearParticle b)
    {
        pool.ReturnObject(b);
    }
}
