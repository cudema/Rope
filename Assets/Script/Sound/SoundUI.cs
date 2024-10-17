using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundUI : MonoBehaviour
{
    SoundManager soundmanager;

    // Start is called before the first frame update
    void Start()
    {
        soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void SetSoundVolume(float _value)
    {
        soundmanager.SetSoundVolume(_value);
        soundmanager.SoundPlay("UI");
    }

    public void SetBgmVolume(float _value)
    {
        soundmanager.SetBgmVolume(_value);

        soundmanager.SoundPlay("UI");
    }

    public void SetMasterVolume(float _value)
    {
        soundmanager.SetMasterVolume(_value);
        soundmanager.SoundPlay("UI");
    }

    public void SoundMute(bool temp)
    {
        soundmanager.SoundMute(temp);
        soundmanager.SoundPlay("UI");
    }

    public void BGMMute(bool temp)
    {
        soundmanager.BGMMute(temp);
        soundmanager.SoundPlay("UI");
    }

    public void MasterMute(bool temp)
    {
        soundmanager.MasterMute(temp);
        soundmanager.SoundPlay("UI");
    }
}
