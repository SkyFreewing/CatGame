using UnityEngine;

namespace CatMerge
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 2)]
    public class GameConfig : ScriptableObject, IGameConfig
    {       
        [SerializeField] Vector2 _boardSize;
        [SerializeField] int _startPlayableCount;
        [SerializeField] int _spawnPlayableCount;
        [SerializeField] int _highestIndexToWin;
        [SerializeField] bool _scoreCounterEnabled;
        [SerializeField] Sprite[] _gradeSprites;
        [SerializeField] Color[] _scoreColors;

        public Vector2 BoardSize => _boardSize;
        public int StartPlayableCount => _startPlayableCount;
        public int SpawnPlayableCount => _spawnPlayableCount;
        public int HighestIndexToWin => _highestIndexToWin;
        public bool ScoreCounterEnabled => _scoreCounterEnabled;
        public Sprite[] GradeSprites => _gradeSprites;
        public Color[] ScoreColors => _scoreColors;
    }
}