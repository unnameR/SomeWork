using UnityEngine.Audio;
using UnityEngine;

[CreateAssetMenu()]
public class SoundSO: ScriptableObject {

    public string _name;
    public AudioClip[] clips;

    public bool loop;
    public bool playOnAwake;
    [Range(0f,1f)]
    public float volume;
    [HideInInspector]
    public AudioSource source;

    public void Init()
    {
        source.playOnAwake = playOnAwake;
        source.volume = volume;
        source.loop = loop;
    }
    public void PlayRandon()
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
    }
    public void Stop()
    {
        source.Stop();
    }
    public void SetVolume(float volume)
    {
        source.volume = volume;
    }
}
