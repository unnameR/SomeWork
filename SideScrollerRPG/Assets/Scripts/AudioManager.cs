using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {
    public static AudioManager _instance;

    public PlayerSO playerParam;

    public Slider soundSlider;
    public Slider musicSlider;

    public SoundSO[] sounds;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        soundSlider.value = playerParam.soundVolume;
        musicSlider.value = playerParam.musicVolume;

    }
    
    public void PlaySoundEffect(string name)
    {
        GetSound(name).PlayRandon();
    }
    public void StopPlay(string name)
    {
        GetSound(name).Stop();
    }
    public void SetVolume(string name, float volume)
    {
        GetSound(name).SetVolume(volume);
    }
    public void ButtonClickEffect()
    {
        PlaySoundEffect("ButtonClick");
    }

    public void SetSoundVolume(float volume)
    {
        AudioListener.volume = volume;
        playerParam.soundVolume = volume;
    }
    public void SetMusicVolume(float volume)
    {
        playerParam.musicVolume = volume;
        SetVolume("MainTheme", volume);
        SetVolume("MenuMusic", volume);
    }

    private SoundSO GetSound(string name)
    {
        SoundSO s = Array.Find<SoundSO>(sounds, sound => sound._name == name);
        if (s.source == null)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.Init();
        }
        return s;
    }
}
