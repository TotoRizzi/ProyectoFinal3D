using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] GameObject _leavingScene;

    public bool changingScene;
    public static SceneManagerScript instance;
    public int currentScene => SceneManager.GetActiveScene().buildIndex;
    public int previousScene => SceneManager.GetActiveScene().buildIndex - 1;
    public int nextScene => SceneManager.GetActiveScene().buildIndex + 1;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public IEnumerator ChangeScene(float timeToFadeOut, int scene)
    {
        changingScene = true;
        yield return new WaitForSeconds(timeToFadeOut);
        _leavingScene.SetActive(true);
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(scene);
    }

    public void PlayerDie() => StartCoroutine(ChangeScene(3f, currentScene));
}
