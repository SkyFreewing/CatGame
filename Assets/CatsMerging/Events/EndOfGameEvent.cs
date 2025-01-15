using System;

namespace CatMerge
{
    internal class EndOfGameEvent
    {
        public static event EventHandler<GameResult> EndOfGame;

        public virtual void OnEndOfGame(GameResult Value) 
        {
            EndOfGame?.Invoke(this, Value);
        }

        public static void AddListener(IEndOfGameListener Listener) 
        {
            EndOfGame += Listener.OnEndOfGame;
        }

        public static void RemoveListener(IEndOfGameListener Listener) 
        {
            EndOfGame -= Listener.OnEndOfGame;
        }      
    }

    public struct GameResult
    {
        bool victory;
        int score;

        public GameResult(bool Victory, int Score)
        {
            victory = Victory;
            score = Score;
        }
    }
}
