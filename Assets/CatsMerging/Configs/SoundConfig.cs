using UnityEngine;

namespace CatMerge
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "ScriptableObjects/SoundConfig", order = 7)]
    public class SoundConfig : ScriptableObject, ISoundConfig
    {
        [SerializeField] GameObject _bgmPrefab;
        [SerializeField] GameObject _soundPrefab;

        [SerializeField] AudioClip _backgroundMusic;
        [SerializeField] AudioClip _lowComboSound;
        [SerializeField] AudioClip _middleComboSound;
        [SerializeField] AudioClip _highComboSound;
        [SerializeField] AudioClip _openUISound;
        [SerializeField] AudioClip _closeUISound;

        [Range(0.0f, 1.0f)]
        [SerializeField] float _masterVolume;
        [SerializeField] float _bgmVolume;
        [SerializeField] float _mergeVolume;
        [SerializeField] float _uiVolume;

        public GameObject BGMSourcePrefab => _bgmPrefab;
        public GameObject SoundSourcePrefab => _soundPrefab;
        public AudioClip BackgroundMusic => _backgroundMusic;
        public AudioClip LowComboSound => _lowComboSound;
        public AudioClip MiddleComboSound => _middleComboSound;
        public AudioClip HighComboSound => _highComboSound;
        public AudioClip OpenUISound => _openUISound;
        public AudioClip CloseUISound => _closeUISound;
        public float MasterVolume => _masterVolume;
        public float BGMVolume => _bgmVolume;
        public float MergeSoundVolume => _mergeVolume;
        public float UISoundVolume => _uiVolume;
    }
}