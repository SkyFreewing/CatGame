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
        public bool Victory;
        public int Score;

        public GameResult(bool victory, int score)
        {
            Victory = victory;
            Score = score;
        }
    }
}
