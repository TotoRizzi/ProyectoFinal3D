using UnityEngine;
public class Menu : MonoBehaviour
{
    public void ChangeScene(int indexScene)
    {
        PausedMenu._gameIsPaused = false;
        StartCoroutine(SceneManagerScript.instance.ChangeScene(0, indexScene));
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
