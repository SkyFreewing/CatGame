using System;

namespace CatMerge
{
    internal class ScoreChangeEvent
    {
        public static event EventHandler<int> ScoreChange;

        public virtual void OnScoreChange(int Value) 
        {
            ScoreChange?.Invoke(this, Value);
        }

        public  static void AddListener(IScoreChangeListener Listener) 
        {
            ScoreChange += Listener.OnScoreChange;
        }

        public static void RemoveListener(IScoreChangeListener Listener) 
        {
            ScoreChange -= Listener.OnScoreChange;
        }
    }
}
