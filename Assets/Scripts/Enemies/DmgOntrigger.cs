using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgOntrigger : MonoBehaviour
{
    [SerializeField] float dmg;
    [SerializeField] bool reloadScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        if (reloadScene) SceneManagerScript.instance.ReloadScene();
        else other.GetComponent<IDamageable>().TakeDamage(dmg);
    }
}
