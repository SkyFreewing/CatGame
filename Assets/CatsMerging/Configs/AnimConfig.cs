using UnityEngine;

namespace CatMerge
{
    [CreateAssetMenu(fileName = "AnimConfig", menuName = "ScriptableObjects/AnimConfig", order = 4)]
    internal class AnimConfig : ScriptableObject, IAnimConfig
    {
        [SerializeField] float _movableAnimationDuration;
        [SerializeField] float _spawnScaleDuration;
        [SerializeField] float _mergingScaleDuration;
        [SerializeField] Vector3 _spawnStartScale;
        [SerializeField] Vector3 _mergingScale;

        public float MovableAnimationDuration => _movableAnimationDuration;
        public float SpawnScaleDuration => _spawnScaleDuration;
        public float MergingScaleDuration => _mergingScaleDuration;
        public Vector3 SpawnStartScale => _spawnStartScale;
        public Vector3 MergingScale => _mergingScale;
    }
}
