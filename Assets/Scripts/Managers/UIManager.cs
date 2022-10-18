using System.Collections;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public System.Action defeatEvent;
    public System.Action victoryEvent;

    [SerializeField] GameObject _defeatPanel;
    [SerializeField] GameObject _victoryPanel;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnEnable()
    {
        defeatEvent += () => StartCoroutine(DefeatPanel());
        victoryEvent += VictoryPanel;
    }
    private void OnDisable()
    {
        defeatEvent -= () => StartCoroutine(DefeatPanel());
        victoryEvent -= VictoryPanel;
    }
    IEnumerator DefeatPanel()
    {
        yield return new WaitForSeconds(3f);
        _defeatPanel.SetActive(true);
    }
    void VictoryPanel()
    {
        Time.timeScale = 0;
        _victoryPanel.SetActive(true);
    }
}
