using System;
using UnityEngine;

namespace CatMerge
{
    internal class EndOfGameSystem : ISystem, IGameplayBlocker, IGameLostListener, IAnyMergeListener, IScoreChangeListener
    {
        int _highestIndexNeededToWin;
        int _currentHighestIndex;
        int _currentScore;

        EndOfGameEvent _endOfGameEvent;

        public EndOfGameSystem(IConfigCatalogue configs)
        {
            _highestIndexNeededToWin = configs.GameConfig.HighestIndexToWin;

            _endOfGameEvent = new EndOfGameEvent();

            GameLostEvent.AddListener(this);
            AnyMergeEvent.AddListener(this);                
        }

        public void OnAnyMerge(object e, int input)
        {
            if (_currentHighestIndex < input)
            {
                _currentHighestIndex = input;

                if (_currentHighestIndex >= _highestIndexNeededToWin)
                {
                    Debug.Log(">>>> Congrats! You have won the game! <<<<");

                    _endOfGameEvent.OnEndOfGame(new GameResult(true, _currentScore));
                }
            }
        }

        public void OnGameLost(object e, bool flag)
        {
            Debug.Log(">>>> Oh no! You have lost the game! <<<<");

            _endOfGameEvent.OnEndOfGame(new GameResult(false, _currentScore));
        }

        public void OnScoreChange(object e, int value)
        {
            _currentScore = value;  
        }
    }
}
