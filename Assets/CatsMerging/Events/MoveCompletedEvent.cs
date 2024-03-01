using System;

namespace CatMerge
{
    internal class MoveCompletedEvent
    {
        public static event EventHandler<bool> MoveCompleted;

        public virtual void OnMoveCompleted() 
        {
            MoveCompleted?.Invoke(this, true);
        }

        public static void AddListener(IMoveCompletedListener listener)
        {
            MoveCompleted += listener.OnMoveCompleted;
        }

        public static void RemoveListener(IMoveCompletedListener listener)
        {
            MoveCompleted -= listener.OnMoveCompleted;
        }
    }
}
