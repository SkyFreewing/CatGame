using System;

namespace CatMerge
{
    internal class AnyMergeEvent
    {
        public static event EventHandler<int> AnyMerge;

        public virtual void OnAnyMerge(int Value)
        {
            AnyMerge?.Invoke(this, Value);
        }

        public static void AddListener(IAnyMergeListener Listener)
        {
            AnyMerge += Listener.OnAnyMerge;
        }

        public static void RemoveListener(IAnyMergeListener Listener)
        {
            AnyMerge -= Listener.OnAnyMerge;
        }
    }
}
