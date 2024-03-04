using UnityEngine;

namespace CatMerge
{
    public interface IAnimConfig
    {
        float MovableAnimationDuration { get; }
        float SpawnScaleDuration { get; }
        Vector3 SpawnStartScale { get; }
    }
}