using UnityEngine;
public class ChangeSceneOnCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player)
        {
            SceneManagerScript sceneManager = SceneManagerScript.instance;
            player.changeScene();
            StartCoroutine(sceneManager.ChangeScene(0, sceneManager.nextScene));
        }
    }
}
