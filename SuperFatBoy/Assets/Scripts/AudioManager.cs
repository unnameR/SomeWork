using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour {
    public static AudioManager _instance;

    public GameParamSO gameParam;

    List<SoundSO> soundsList = new List<SoundSO>();

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(this);
            return;
        }
    }

    public void PlaySoundEffect(SoundSO sound)
    {
        GetSound(sound).PlayRandon();
    }
    public void StopPlay(SoundSO sound)
    {
        GetSound(sound).Stop();
    }
    public void SetVolume(SoundSO sound, float volume)
    {
        GetSound(sound).SetVolume(volume);
    }

    public void SetSoundVolume(float volume)
    {
        AudioListener.volume = volume;
        //playerParam.soundVolume = volume;
    }

    private SoundSO GetSound(SoundSO sound)
    {
        if(!soundsList.Contains(sound))
        {
            soundsList.Add(sound);
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.Init();
        }
        return sound;
    }
}
