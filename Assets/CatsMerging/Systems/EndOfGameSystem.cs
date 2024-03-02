using System;
using UnityEngine;

namespace CatMerge
{
    internal class EndOfGameSystem : ISystem, IGameLostListener, IAnyMergeListener
    {
        int _highestIndexNeededToWin;
        int _currentHighestIndex;

        public EndOfGameSystem(IConfigCatalogue configs)
        {
            _highestIndexNeededToWin = configs.GameConfig.HighestIndexToWin;

            GameLostEvent.AddListener(this);
            AnyMergeEvent.AddListener(this);                
        }

        public void OnAnyMerge(object e, int input)
        {
            _currentHighestIndex = (int)MathF.Max(input, _currentHighestIndex);

            if (_currentHighestIndex >= _highestIndexNeededToWin) 
            {
                Debug.Log(">>>> Congrats! You have won the game! <<<<");
                //TODO: Create Blockers for Gameplay here!
            }
        }

        public void OnGameLost(object e, bool flag)
        {
            Debug.Log(">>>> Oh no! You have lost the game! <<<<");
            //TODO: Create Blockers for Gameplay here!
        }
    }
}
