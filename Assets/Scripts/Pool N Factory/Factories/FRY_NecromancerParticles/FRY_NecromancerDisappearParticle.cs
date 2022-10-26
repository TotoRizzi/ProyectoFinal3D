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

    public Particle particlePrefab;
    public int objectStock = 2;

    public ObjectPool<Particle> pool;

    void Start()
    {
        _instance = this;
        pool = new ObjectPool<Particle>(ObjectCreator, Particle.TurnOn, Particle.TurnOff, objectStock);
    }

    public Particle ObjectCreator()
    {
        return Instantiate(particlePrefab);
    }

    public void ReturnObject(Particle b)
    {
        pool.ReturnObject(b);
    }
}
