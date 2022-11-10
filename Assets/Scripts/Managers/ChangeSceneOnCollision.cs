using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManagerScript.instance.NextScene();
    }
}
