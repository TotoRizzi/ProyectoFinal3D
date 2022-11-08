using UnityEngine;
using UnityEngine.UI;
public class UIAudioController : MonoBehaviour
{
    [SerializeField] Slider _globalSlider, _musicSlider, _sfxSlider;
    [SerializeField] Button _globalButton, _musicButton, _sfxButton;
    [SerializeField] Sprite[] _toggleSprites;

    AudioManager _audioManager;
    private void Start()
    {
        _audioManager = AudioManager.Instance;
        _audioManager.ChangeMasterVolume(_globalSlider.value);
        _globalSlider.onValueChanged.AddListener(val => _audioManager.ChangeMasterVolume(val));
        _musicSlider.onValueChanged.AddListener(val => _audioManager.MusicVolume(val));
        _sfxSlider.onValueChanged.AddListener(val => _audioManager.SFXVolume(val));
    }

    public void ToggleMusic()
    {
        _audioManager.ToggleMusic(_musicButton, _toggleSprites[0], _toggleSprites[1]);
    }
    public void ToggleSFX()
    {
        _audioManager.ToggleSFX(_sfxButton, _toggleSprites[0], _toggleSprites[1]);
    }
    public void ToggleGlobal()
    {
        _audioManager.ToggleGlobal(_globalButton, _toggleSprites[0], _toggleSprites[1]);
    }
    public void ChangeMasterVolume()
    {
        _audioManager.ChangeMasterVolume(_globalSlider.value);
    }
    public void MusicVolume()
    {
        _audioManager.MusicVolume(_musicSlider.value);
    }
    public void SFXVolume()
    {
        _audioManager.SFXVolume(_sfxSlider.value);
    }
}
