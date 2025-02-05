using System;

namespace CatMerge
{
    internal class ResetGameEvent
    {
        public static event EventHandler<bool> ResetGame;

        public virtual void OnResetGame(bool Flag)
        {
            ResetGame?.Invoke(this, Flag);
        }

        public static void AddListener(IResetGameListener Listener)
        {
            ResetGame += Listener.OnResetGame;
        }

        public static void RemoveListener(IResetGameListener Listener)
        {
            ResetGame -= Listener.OnResetGame;
        }
    }
}
