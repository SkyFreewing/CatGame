using System;

namespace CatMerge
{
    internal class GameplayBlockerRemovedEvent
    {
        public static event EventHandler<IGameplayBlocker> GameplayBlockerRemoved;

        public virtual void OnGameplayBlockerRemoved(IGameplayBlocker blocker)
        {
            GameplayBlockerRemoved?.Invoke(this, blocker);
        }

        public static void AddListener(IGameplayBlockerRemovedListener listener)
        {
            GameplayBlockerRemoved += listener.OnGameplayBlockerRemoved;
        }

        public static void RemoveListener(IGameplayBlockerRemovedListener listener)
        {
            GameplayBlockerRemoved -= listener.OnGameplayBlockerRemoved;
        }
    }
}
