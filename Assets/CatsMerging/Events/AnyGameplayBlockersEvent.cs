using System;
using System.Collections.Generic;

namespace CatMerge
{
    internal class AnyGameplayBlockersEvent
    {
        public static event EventHandler<List<IGameplayBlocker>> AnyGameplayBlockers;

        public virtual void OnAnyGameplayBlockers(List<IGameplayBlocker> blockers)
        {
            AnyGameplayBlockers?.Invoke(this, blockers);
        }

        public static void AddListener(IAnyGameplayBlockersListener listener)
        {
            AnyGameplayBlockers += listener.OnAnyGameplayBlockers;
        }

        public static void RemoveListener(IAnyGameplayBlockersListener listener)
        {
            AnyGameplayBlockers -= listener.OnAnyGameplayBlockers;
        }
    }
}
