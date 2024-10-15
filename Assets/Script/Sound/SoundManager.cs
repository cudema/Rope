using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
class Sound
{
    public string name;
    [SerializeField]
    AudioClip audioClip;
    [SerializeField]
    bool loop;
    [SerializeField, Range(0, 1)]
    float volume;

    AudioSource source;

    public void SetSource(AudioSource source)
    {
        this.source = source;
        this.source.clip = audioClip;
        this.source.loop = loop;
        this.source.volume = volume;
    }

    public void SetVolume(float volume)
    {
        this.volume = Mathf.Clamp(volume, 0, 1);
    }

    public void Play()
    {
        source.Play();
    }

    public void Stop() 
    { 
        source.Stop();
    }
}
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    Sound[] soundGroup;

    private void Start()
    {
        for (int i = 0; i < soundGroup.Length; i++)
        {
            GameObject soundObject = new GameObject(soundGroup[i].name);
            soundGroup[i].SetSource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }
    }

    public void SoundPlay(string _name)
    {
        for (int i = 0; i < soundGroup.Length; i++)
        {
            if (_name == soundGroup[i].name)
            {
                soundGroup[i].Play();
                return;
            }
        }
    }

    public void SoundStop(string _name)
    {
        for (int i = 0; i < soundGroup.Length; i++)
        {
            if (_name == soundGroup[i].name)
            {
                soundGroup[i].Stop();
                return;
            }
        }
    }

    public void SetSoundVolume(float _value)
    {
        for (int i = 0; i < soundGroup.Length; i++)
        {
            soundGroup[i].SetVolume(_value);
        }
    }
}