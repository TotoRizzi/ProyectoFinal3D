using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PT_DisappearingPlatform : Particle
{
    public override void ReturnToFactory()
    {
        FRY_DisappearingPlatformParticle.Instance.ReturnObject(this);
    }
    public PT_DisappearingPlatform SetPosition(Vector3 pos)
    {
        transform.position = pos;
        return this;
    }
}
