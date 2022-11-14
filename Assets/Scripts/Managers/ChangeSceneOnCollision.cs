using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneOnCollision : MonoBehaviour
{
    [SerializeField] GameObject _leavingScene;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        _leavingScene.SetActive(true);
        yield return new WaitForSeconds(1);

        SceneManagerScript.instance.NextScene();
    }
}
