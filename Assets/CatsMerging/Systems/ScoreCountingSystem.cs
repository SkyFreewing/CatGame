using System;

namespace CatMerge
{
    internal class ScoreCountingSystem : ISystem, IStartupSystem, IAnyMergeListener
    {
        float _scoreMultiplier;
        int _currentScore;

        ScoreChangeEvent _scoreChangeEvent;

        public ScoreCountingSystem(IConfigCatalogue configs)
        {
            AnyMergeEvent.AddListener(this);

            _scoreChangeEvent = new ScoreChangeEvent();
        }

        public void Startup()
        {
            _currentScore = 0;
        }

        public void OnAnyMerge(object e, int input)
        {
            var updatedScore = (int)Math.Ceiling(_currentScore + input * _scoreMultiplier);
            _currentScore = updatedScore;
            _scoreChangeEvent.OnScoreChange(updatedScore);
        }
    }
}
