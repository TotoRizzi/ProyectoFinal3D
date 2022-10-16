using UnityEngine;
public class PausedMenu : Menu
{
    public static bool _gameIsPaused;

    [SerializeField] GameObject _pauseGM;
    [SerializeField] GameObject _controlsMenu;
    void Start()
    {
        ResumeGame();
        _pauseGM.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _controlsMenu.SetActive(false);
            if (_gameIsPaused)
                ResumeGame();
            else
                PauseGame();
        }

    }
    public void PauseGame()
    {
        _gameIsPaused = true;
        Time.timeScale = 0;
        _pauseGM.SetActive(true);
    }
    public void ResumeGame()
    {
        _gameIsPaused = false;
        Time.timeScale = 1;
        _pauseGM.SetActive(false);
    }
}
