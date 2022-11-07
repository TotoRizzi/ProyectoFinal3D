using System;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
            Debug.Log("Sound Not Found!");
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
            Debug.Log("Sound Not Found");
        else
            sfxSource.PlayOneShot(s.clip);
    }
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
    public void ToggleSFX(Button button, Sprite mute, Sprite unMute)
    {
        sfxSource.mute = !sfxSource.mute;
        button.GetComponent<Image>().sprite = sfxSource.mute ? mute : unMute;
    }
    public void ToggleMusic(Button button, Sprite mute, Sprite unMute)
    {
        musicSource.mute = !musicSource.mute;
        button.GetComponent<Image>().sprite = musicSource.mute ? mute : unMute;
    }
    public void ToggleGlobal(Button button, Sprite mute, Sprite unMute)
    {
        AudioListener.pause = !AudioListener.pause;
        button.GetComponent<Image>().sprite = AudioListener.pause ? mute : unMute;
    }
    public void MusicVolume(float value)
    {
        musicSource.volume = value;
    }
    public void SFXVolume(float value)
    {
        sfxSource.volume = value;
    }
}
