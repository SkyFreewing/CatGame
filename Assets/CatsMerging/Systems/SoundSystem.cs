using CatMerge;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : IStartupSystem, IAnyMergeListener
{
    public static SoundSystem Instance { get; private set; }

    //TODO: Setup SoundConfig and save resources there
    const string _bgmPrefabPath = "Prefabs/BGMSoundSource";
    const string _soundPrefabPath = "Prefabs/SoundSource";

    const string _comboClipOnePrefabPath = "Sound/Combo1";
    const string _comboClipTwoPrefabPath = "Sound/Combo2";
    const string _comboClipThreePrefabPath = "Sound/Combo3";
    const string _openUIClipPrefabPath = "Sound/Maximize";
    const string _closeUIClipPrefabPath = "Sound/Minimize";

    IGameConfig _config;
    Object _bgmResource;
    Object _soundResource;
    GameObject _soundGO;
    GameObject _bgmGO;
    List<AudioClip> _audioClips = new List<AudioClip>(); 

    public SoundSystem(IConfigCatalogue configs)
    {
        _config = configs.GameConfig;
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

        _audioClips.Add((AudioClip)Resources.Load(_comboClipOnePrefabPath));
        _audioClips.Add((AudioClip)Resources.Load(_comboClipTwoPrefabPath));
        _audioClips.Add((AudioClip)Resources.Load(_comboClipThreePrefabPath));
        _audioClips.Add((AudioClip)Resources.Load(_openUIClipPrefabPath));
        _audioClips.Add((AudioClip)Resources.Load(_closeUIClipPrefabPath));

        //Create the background music
        _bgmGO = GameObject.Instantiate(_bgmResource, _soundGO.transform) as GameObject;
        var audioSource = _bgmGO.GetComponent<AudioSource>();
        audioSource.volume = _config.BGMVolume;
        audioSource.Play();
    }

    public void OnAnyMerge(object e, int input)
    {
        var soundGO = GameObject.Instantiate(_soundResource) as GameObject;

        var audioSource = soundGO.GetComponent<AudioSource>();
        audioSource.volume = _config.MergeSoundVolume;

        if (input < 3)
        {
            audioSource.clip = _audioClips[0];
        }
        else if (input < 7)
        {
            audioSource.clip = _audioClips[1];
            audioSource.volume = audioSource.volume + 0.02f;
        }
        else
        {
            audioSource.clip = _audioClips[2];
            audioSource.volume = audioSource.volume + 0.06f;
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
