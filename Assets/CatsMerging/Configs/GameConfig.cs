using UnityEngine;

namespace CatMerge
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 2)]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        [SerializeField] Vector2 _boardSize;
        [SerializeField] int _startPlayableCount;

        public Vector2 BoardSize => _boardSize;
        public int StartPlayableCount => _startPlayableCount;
    }
}