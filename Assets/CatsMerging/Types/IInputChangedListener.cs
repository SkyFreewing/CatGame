using UnityEngine;

namespace CatMerge
{
    internal interface IInputChangedListener
    {
        void OnInputChanged(object e, Vector2 input);
    }
}
