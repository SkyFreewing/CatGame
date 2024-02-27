using UnityEngine;

namespace CatMerge
{
    [CreateAssetMenu(fileName = "AnimConfig", menuName = "ScriptableObjects/AnimConfig", order = 4)]
    internal class AnimConfig : ScriptableObject, IAnimConfig
    {
        [SerializeField] float _movableAnimationDuration;

        public float MovableAnimationDuration => _movableAnimationDuration;
    }
}
