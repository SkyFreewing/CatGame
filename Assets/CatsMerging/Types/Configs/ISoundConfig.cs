using UnityEngine;

namespace CatMerge
{
    public interface ISoundConfig
    {
        GameObject BGMSourcePrefab { get; }
        GameObject SoundSourcePrefab { get; }
        AudioClip BackgroundMusic { get; }
        AudioClip LowComboSound { get; }
        AudioClip MiddleComboSound { get; }
        AudioClip HighComboSound { get; }
        AudioClip OpenUISound { get; }
        AudioClip CloseUISound { get; }
        [Range(0.0f, 1.0f)]
        float MasterVolume { get; }
        float BGMVolume { get; }
        float MergeSoundVolume { get; }
        float UISoundVolume { get; }
    }
}