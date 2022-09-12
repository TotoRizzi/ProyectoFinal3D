using UnityEngine;
public class PausedMenu : Menu
{
    public static bool _gameIsPaused;

    [SerializeField] GameObject _pauseGM;
    void Start()
    {
        _pauseGM.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameIsPaused)
                ResumeGame();
            else
                PauseGame();
        }

    }
    public void PauseGame()
    {
        _gameIsPaused = true;
        _pauseGM.SetActive(true);
    }
    public void ResumeGame()
    {
        _gameIsPaused = false;
        _pauseGM.SetActive(false);
    }
}
