using UnityEngine;

namespace CatMerge
{
    [CreateAssetMenu(fileName = "AnimConfig", menuName = "ScriptableObjects/AnimConfig", order = 4)]
    internal class AnimConfig : ScriptableObject, IAnimConfig
    {
        [SerializeField] float _movableAnimationDuration;
        [SerializeField] float _spawnScaleDuration;
        [SerializeField] Vector3 _spawnStartScale;

        public float MovableAnimationDuration => _movableAnimationDuration;
        public float SpawnScaleDuration => _spawnScaleDuration;
        public Vector3 SpawnStartScale => _spawnStartScale;
    }
}
