using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRY_DisappearingPlatformParticle : MonoBehaviour
{
    public static FRY_DisappearingPlatformParticle Instance
    {
        get
        {
            return _instance;
        }
    }
    static FRY_DisappearingPlatformParticle _instance;


    public PT_DisappearingPlatform deadRavenParticlePrefab;
    public int particleStock = 2;

    public ObjectPool<PT_DisappearingPlatform> pool;


    void Start()
    {
        _instance = this;
        pool = new ObjectPool<PT_DisappearingPlatform>(ObjectCreator, PT_DisappearingPlatform.TurnOn, PT_DisappearingPlatform.TurnOff, particleStock);
    }

    public PT_DisappearingPlatform ObjectCreator()
    {
        return Instantiate(deadRavenParticlePrefab);
    }

    public void ReturnObject(PT_DisappearingPlatform b)
    {
        pool.ReturnObject(b);
    }
}
