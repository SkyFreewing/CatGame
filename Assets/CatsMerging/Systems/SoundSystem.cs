using CatMerge;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : IStartupSystem, IAnyMergeListener
{
    public static SoundSystem Instance { get; private set; }

    const string _bgmPrefabPath = "Prefabs/BGMSoundSource";
    const string _soundPrefabPath = "Prefabs/SoundSource";

    ISoundConfig _config;
    Object _bgmResource;
    Object _soundResource;
    GameObject _soundGO;
    GameObject _bgmGO;
    List<AudioClip> _audioClips = new List<AudioClip>(); 

    public SoundSystem(IConfigCatalogue configs)
    {
        _config = configs.SoundConfig;
        AnyMergeEvent.AddListener(this);
        Instance = this;
    }

    public void Startup()
    {
        //Parent GameObject for any sound
        _soundGO = new GameObject("Sounds");

        //Load all sound resources ahead of time
        _bgmResource = Resources.Load(_bgmPrefabPath);
        _soundResource = Resources.Load(_soundPrefabPath);

        _audioClips.Add(_config.LowComboSound);
        _audioClips.Add(_config.MiddleComboSound);
        _audioClips.Add(_config.HighComboSound);
        _audioClips.Add(_config.OpenUISound);
        _audioClips.Add(_config.CloseUISound);

        //Create the background music
        _bgmGO = GameObject.Instantiate(_bgmResource, _soundGO.transform) as GameObject;
        var audioSource = _bgmGO.GetComponent<AudioSource>();
        audioSource.volume = _config.BGMVolume * _config.MasterVolume;
        audioSource.Play();
    }

    public void OnAnyMerge(object e, int input)
    {
        var soundGO = GameObject.Instantiate(_soundResource) as GameObject;

        var audioSource = soundGO.GetComponent<AudioSource>();
        audioSource.volume = _config.MergeSoundVolume * _config.MasterVolume;

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

        audioSource.volume = _config.UISoundVolume;      
        audioSource.clip = open ? _audioClips[3] : _audioClips[4];
        audioSource.Play();
    }

    public void DampenBGMVolume(bool dampen) 
    {
        var audioSource = _bgmGO.GetComponent<AudioSource>();
        audioSource.volume = dampen ? audioSource.volume / 4 : _config.BGMVolume;
    }
}
