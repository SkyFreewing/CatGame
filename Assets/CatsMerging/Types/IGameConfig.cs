using UnityEngine;

namespace CatMerge
{
    public interface IGameConfig
    {
        Vector2 BoardSize { get; }
        int StartPlayableCount { get; }
    }
}