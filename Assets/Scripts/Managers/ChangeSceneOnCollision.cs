using System.Collections;
using UnityEngine;

public class ChangeSceneOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            SceneManagerScript sceneManager = SceneManagerScript.instance;
            StartCoroutine(sceneManager.ChangeScene(0, sceneManager.nextScene));
        }
    }
}
