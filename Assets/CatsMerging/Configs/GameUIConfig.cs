using UnityEngine;

namespace CatMerge
{
    [CreateAssetMenu(fileName = "GameUIConfig", menuName = "ScriptableObjects/GameUIConfig", order = 5)]
    internal class GameUIConfig : ScriptableObject, IGameUIConfig
    {
        [SerializeField] RenderMode _gameCanvasRenderMode;
        [SerializeField] bool _scoreCounterEnabled;
        [SerializeField] bool _customCursorEnabled;

        public RenderMode GameCanvasRenderMode => _gameCanvasRenderMode;
        public bool ScoreCounterEnabled => _scoreCounterEnabled;
        public bool CustomCursorEnabled => _customCursorEnabled;
    }
}
