using UnityEngine;

namespace CatMerge
{
    public interface IAnimConfig
    {
        float MovableAnimationDuration { get; }
        float SpawnScaleDuration { get; }
        float MergingScaleDuration { get; }
        Vector3 SpawnStartScale { get; }
        Vector3 MergingScale { get; }
    }
}