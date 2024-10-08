using UnityEngine;

namespace CatMerge
{
    public interface IGameConfig
    {
        Vector2 BoardSize { get; }
        int StartPlayableCount { get; }
        int SpawnPlayableCount { get; }
        int HighestIndexToWin { get; }
        Color[] GradeColors { get; }
    }
}