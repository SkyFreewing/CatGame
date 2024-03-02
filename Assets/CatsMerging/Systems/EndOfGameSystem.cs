using System;
using UnityEngine;

namespace CatMerge
{
    internal class EndOfGameSystem : ISystem, IGameplayBlocker, IGameLostListener, IAnyMergeListener
    {
        int _highestIndexNeededToWin;
        int _currentHighestIndex;

        GameplayBlockerAddedEvent _gameplayBlockerAddedEvent;

        public EndOfGameSystem(IConfigCatalogue configs)
        {
            _highestIndexNeededToWin = configs.GameConfig.HighestIndexToWin;

            _gameplayBlockerAddedEvent = new GameplayBlockerAddedEvent();

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

                    _gameplayBlockerAddedEvent.OnGameplayBlockerAdded(this);
                }
            }
        }

        public void OnGameLost(object e, bool flag)
        {
            Debug.Log(">>>> Oh no! You have lost the game! <<<<");

            _gameplayBlockerAddedEvent.OnGameplayBlockerAdded(this);
        }
    }
}
