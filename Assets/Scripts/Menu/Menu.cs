using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void ChangeScene(int indexScene)
    {
        PausedMenu._gameIsPaused = false;
        SceneManager.LoadScene(indexScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
