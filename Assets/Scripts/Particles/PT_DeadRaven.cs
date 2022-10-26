using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PT_DeadRaven : Particle
{
    public override void ReturnToFactory()
    {
        FRY_DeadRavenParticle.Instance.ReturnObject(this);
    }
    public PT_DeadRaven SetPosition(Vector3 pos)
    {
        transform.position = pos;
        return this;
    }
}
