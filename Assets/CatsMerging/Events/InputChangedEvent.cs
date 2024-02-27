using System;
using UnityEngine;

namespace CatMerge
{
    internal class InputChangedEvent
    {
        public static event EventHandler<Vector2> InputChanged;

        public virtual void OnInputChanged(Vector2 vector) 
        {
            InputChanged?.Invoke(this, vector);
        }

        public static void AddListener(IInputChangedListener listener)
        {
            InputChanged += listener.OnInputChanged;
        }

        public static void RemoveListener(IInputChangedListener listener)
        {
            InputChanged -= listener.OnInputChanged;
        }
    }
}
