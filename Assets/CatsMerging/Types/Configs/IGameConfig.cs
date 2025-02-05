using UnityEngine;

namespace CatMerge
{
    public interface IGameConfig
    {
        Vector2 BoardSize { get; }
        int StartPlayableCount { get; }
        int SpawnPlayableCount { get; }
        int HighestIndexToWin { get; }
        bool ScoreCounterEnabled { get; }
        float BGMVolume { get; }
        float MergeSoundVolume { get; }
        float UISoundVolume { get; }
        Sprite[] GradeSprites { get; }
        Color[] ScoreColors { get; }
    }
}