using System;

namespace CatMerge
{
    internal class AnyMergeEvent
    {
        public static event EventHandler<int> AnyMerge;

        public virtual void OnAnyMerge(int input)
        {
            AnyMerge?.Invoke(this, input);
        }

        public static void AddListener(IAnyMergeListener listener)
        {
            AnyMerge += listener.OnAnyMerge;
        }

        public static void RemoveListener(IAnyMergeListener listener)
        {
            AnyMerge -= listener.OnAnyMerge;
        }
    }
}
