using System;
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
    [SerializeField]
    bool mute;
    [SerializeField, Range(0, 1)]
    float volume;

    AudioSource source;

    public void SetSource(AudioSource source)
    {
        this.source = source;
        this.source.clip = audioClip;
        this.source.loop = loop;
        this.source.volume = volume;
        this.source.mute = this.mute;
    }

    public void SetVolume(float volume)
    {
        this.volume = Mathf.Clamp(volume, 0, 1);
        source.volume = this.volume;
    }

    public void Play()
    {
        source.volume = volume;
        source.Play();
    }

    public void Stop() 
    { 
        source.Stop();
    }

    public void SetMute(bool mute)
    {

        this.mute = !mute;
        source.mute = this.mute;
    }
}
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    Sound[] soundGroup;
    [SerializeField]
    Sound[] BGMGroup;

    [SerializeField, Range(0, 1)]
    float masterVolume;
    [SerializeField, Range(0, 1)]
    float soundVolume;
    [SerializeField, Range(0, 1)]
    float BGMVolume;

    static GameObject soundManager;

    private void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(soundManager);
    }

    private void Start()
    {
        for (int i = 0; i < soundGroup.Length; i++)
        {
            GameObject soundObject = new GameObject(soundGroup[i].name);
            soundGroup[i].SetSource(soundObject.AddComponent<AudioSource>());
            soundGroup[i].SetVolume(soundVolume);
            soundObject.transform.SetParent(this.transform);
        }

        for (int i = 0; i < BGMGroup.Length; i++)
        {
            GameObject soundObject = new GameObject(BGMGroup[i].name);
            BGMGroup[i].SetSource(soundObject.AddComponent<AudioSource>());
            BGMGroup[i].SetVolume(BGMVolume);
            soundObject.transform.SetParent(this.transform);
        }

        SetSoundVolume(soundVolume);
        SetBgmVolume(BGMVolume);

        PlayBGM("MainMenu");
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

    public void PlayBGM(string _name)
    {
        for (int i = 0; i < BGMGroup.Length; i++)
        {
            if (_name == BGMGroup[i].name)
            {
                BGMGroup[i].Play();
            }
            else
            {
                BGMGroup[i].Stop();
            }
        }
    }

    public void SetSoundVolume(float _value)
    {
        soundVolume = _value;
        for (int i = 0; i < soundGroup.Length; i++)
        {
            soundGroup[i].SetVolume(masterVolume * soundVolume);
        }
    }

    public void SetBgmVolume(float _value)
    {
        BGMVolume = _value;
        for (int i = 0; i < BGMGroup.Length; i++)
        {
            BGMGroup[i].SetVolume(masterVolume * BGMVolume);
        }
    }

    public void SetMasterVolume(float _value)
    {
        masterVolume = _value;
        for (int i = 0; i < soundGroup.Length; i++)
        {
            soundGroup[i].SetVolume(masterVolume * soundVolume);
        }
        for (int i = 0; i < BGMGroup.Length; i++)
        {
            BGMGroup[i].SetVolume(masterVolume * BGMVolume);
        }
    }

    public void SoundMute(bool temp)
    {
        for (int i = 0; i < soundGroup.Length; i++)
        {
            soundGroup[i].SetMute(temp);
        }
    }

    public void BGMMute(bool temp)
    {
        for (int i = 0; i < BGMGroup.Length; i++)
        {
            BGMGroup[i].SetMute(temp);
        }
    }

    public void MasterMute(bool temp)
    {
        SoundMute(temp);
        BGMMute(temp);
    }
}