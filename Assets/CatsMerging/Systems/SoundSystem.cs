using CatMerge;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : IStartupSystem, IAnyMergeListener
{
    public static SoundSystem Instance { get; private set; }

    float _masterVolume = 1.0f;
    ISoundConfig _config;
    GameObject _bgmResource;
    GameObject _soundResource;
    GameObject _soundGO;
    GameObject _bgmGO;
    List<AudioClip> _audioClips = new List<AudioClip>(); 

    public SoundSystem(IConfigCatalogue configs)
    {
        _config = configs.SoundConfig;
        _masterVolume = _config.MasterVolume;
        AnyMergeEvent.AddListener(this);
        Instance = this;
    }

    public void Startup()
    {
        //Parent GameObject for any sound
        _soundGO = new GameObject("Sounds");

        //Sound Prefabs with playback settings
        _bgmResource = _config.BGMSourcePrefab;
        _soundResource = _config.SoundSourcePrefab;

        //Load all sound resources ahead of time
        _audioClips.Add(_config.LowComboSound);
        _audioClips.Add(_config.MiddleComboSound);
        _audioClips.Add(_config.HighComboSound);
        _audioClips.Add(_config.OpenUISound);
        _audioClips.Add(_config.CloseUISound);

        //Create the background music
        _bgmGO = GameObject.Instantiate(_bgmResource, _soundGO.transform) as GameObject;
        var audioSource = _bgmGO.GetComponent<AudioSource>();
        audioSource.volume = _config.BGMVolume * _masterVolume;
        audioSource.Play();
    }

    public void OnAnyMerge(object e, int input)
    {
        var soundGO = GameObject.Instantiate(_soundResource) as GameObject;

        var audioSource = soundGO.GetComponent<AudioSource>();
        audioSource.volume = _config.MergeSoundVolume * _masterVolume;

        if (input < 3)
        {
            audioSource.clip = _audioClips[0];
        }
        else if (input < 7)
        {
            audioSource.clip = _audioClips[1];
            audioSource.volume = audioSource.volume * 1.2f;
        }
        else
        {
            audioSource.clip = _audioClips[2];
            audioSource.volume = audioSource.volume * 1.6f;
        }

        audioSource.Play();        
    }

    public void PlayUISound(bool open)
    {
        var soundGO = GameObject.Instantiate(_soundResource) as GameObject;
        var audioSource = soundGO.GetComponent<AudioSource>();

        audioSource.volume = _config.UISoundVolume * _masterVolume;
        audioSource.clip = open ? _audioClips[3] : _audioClips[4];
        audioSource.Play();
    }

    public void DampenBGMVolume(bool dampen) 
    {
        var audioSource = _bgmGO.GetComponent<AudioSource>();
        audioSource.volume = dampen ? audioSource.volume / 4 : _config.BGMVolume * _masterVolume;
    }

    public void SetVolume(float volume)
    {
        _masterVolume = volume;
        var audioSource = _bgmGO.GetComponent<AudioSource>();
        audioSource.volume = _config.BGMVolume * _masterVolume;
    }
}
