using System;

namespace CatMerge
{
    internal class GameLostEvent
    {
        public static event EventHandler<bool> GameLost;

        public virtual void OnGameLost() 
        {
            GameLost?.Invoke(this, true);
        }

        public static void AddListener(IGameLostListener listener)
        {
            GameLost += listener.OnGameLost;
        }

        public static void RemoveListener(IGameLostListener listener)
        {
            GameLost -= listener.OnGameLost;
        }
    }
}
