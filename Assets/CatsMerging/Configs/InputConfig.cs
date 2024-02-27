using UnityEngine;

namespace CatMerge
{
    [CreateAssetMenu(fileName = "InputConfig", menuName = "ScriptableObjects/InputConfig", order = 3)]
    internal class InputConfig : ScriptableObject, IInputConfig
    {
        [SerializeField] float _swipeCorrectnessThreshold;
        [SerializeField] float _swipeMinimumDistance;

        public float SwipeCorrectnessThreshold => _swipeCorrectnessThreshold;
        public float SwipeMinimumDistance => _swipeMinimumDistance;
    }
}
