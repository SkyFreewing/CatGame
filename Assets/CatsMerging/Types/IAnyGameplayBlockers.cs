using System.Collections.Generic;

namespace CatMerge
{
    internal interface IAnyGameplayBlockersListener
    {
        public void OnAnyGameplayBlockers(object e, List<IGameplayBlocker> blockers);
    }
}
