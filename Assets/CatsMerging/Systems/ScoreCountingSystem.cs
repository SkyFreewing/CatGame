using System;

namespace CatMerge
{
    internal class ScoreCountingSystem : IStartupSystem, IAnyMergeListener, IResetGameListener
    {
        int _currentScore;
        ScoreChangeEvent _scoreChangeEvent;

        public ScoreCountingSystem(IConfigCatalogue configs)
        {         
            _scoreChangeEvent = new ScoreChangeEvent();

            AnyMergeEvent.AddListener(this);
            ResetGameEvent.AddListener(this);
        }

        public void Startup()
        {
            _currentScore = 0;
        }

        public void OnAnyMerge(object e, int input)
        {
            var multipliedInput = Math.Pow(2, input);
            var updatedScore = (int)Math.Ceiling(_currentScore + multipliedInput);
            _currentScore = updatedScore;
            _scoreChangeEvent.OnScoreChange(updatedScore);
        }

        public void OnResetGame(object e, bool flag)
        {
            _currentScore = 0;
            _scoreChangeEvent.OnScoreChange(_currentScore);
        }
    }
}
