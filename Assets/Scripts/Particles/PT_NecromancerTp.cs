using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PT_NecromancerTp : Particle
{
    public override void ReturnToFactory()
    {
        FRY_NecromancerTpParicle.Instance.ReturnObject(this);
    }
    public PT_NecromancerTp SetPosition(Vector3 pos)
    {
        transform.position = pos;
        return this;
    }
}
