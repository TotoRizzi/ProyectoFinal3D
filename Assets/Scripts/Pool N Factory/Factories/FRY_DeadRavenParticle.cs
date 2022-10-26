using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRY_DeadRavenParticle : MonoBehaviour
{
    public static FRY_DeadRavenParticle Instance
    {
        get
        {
            return _instance;
        }
    }
    static FRY_DeadRavenParticle _instance;


    public PT_DeadRaven deadRavenParticlePrefab;
    public int particleStock = 2;

    public ObjectPool<PT_DeadRaven> pool;


    void Start()
    {
        _instance = this;
        pool = new ObjectPool<PT_DeadRaven>(ObjectCreator, PT_DeadRaven.TurnOn, PT_DeadRaven.TurnOff, particleStock);
    }

    public PT_DeadRaven ObjectCreator()
    {
        return Instantiate(deadRavenParticlePrefab);
    }

    public void ReturnObject(PT_DeadRaven b)
    {
        pool.ReturnObject(b);
    }
}
