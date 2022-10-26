using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PT_NecromancerDisappearParticle : Particle
{
    public override void ReturnToFactory()
    {
        FRY_NecromancerDisappearParticle.Instance.ReturnObject(this);
    }
    public PT_NecromancerDisappearParticle SetPosition(Vector3 pos)
    {
        transform.position = pos;
        return this;
    }
}
