using System;

namespace CatMerge
{
    internal class GameplayBlockerAddedEvent
    {
        public static event EventHandler<IGameplayBlocker> GameplayBlockerAdded;

        public virtual void OnGameplayBlockerAdded(IGameplayBlocker blocker)
        {
            GameplayBlockerAdded?.Invoke(this, blocker);
        }

        public static void AddListener(IGameplayBlockerAddedListener listener)
        {
            GameplayBlockerAdded += listener.OnGameplayBlockerAdded;
        }

        public static void RemoveListener(IGameplayBlockerAddedListener listener)
        {
            GameplayBlockerAdded -= listener.OnGameplayBlockerAdded;
        }
    }
}
