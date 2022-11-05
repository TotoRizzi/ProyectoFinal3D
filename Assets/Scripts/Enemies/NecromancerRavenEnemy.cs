using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerRavenEnemy : SimpleRavenEnemy
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

    public static void TurnOn(SimpleRavenEnemy b)
    {
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(SimpleRavenEnemy b)
    {
        b.gameObject.SetActive(false);
    }

    public SimpleRavenEnemy SetPosition(Vector3 pos)
    {
        transform.position = pos;
        return this;
    }
    #endregion
}
