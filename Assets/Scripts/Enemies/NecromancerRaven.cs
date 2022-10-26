using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerRaven : SimpleRaven
{
    public override void Die()
    {
        base.Die();
        ReturnToFactory();
    }
    #region Factory
    public virtual void ReturnToFactory()
    {
        FRY_NecromancerRaven.Instance.ReturnObject(this);
    }

    public static void TurnOn(SimpleRaven b)
    {
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(SimpleRaven b)
    {
        b.gameObject.SetActive(false);
    }

    public SimpleRaven SetPosition(Vector3 pos)
    {
        transform.position = pos;
        return this;
    }
    #endregion
}
