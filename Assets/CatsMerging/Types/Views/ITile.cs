using UnityEngine;

namespace CatMerge
{
    internal interface ITile
    {
        bool IsOccupied { get; set; }
        Vector3 Position { get; set; }
        void SetPosition(Vector3 NewPosition);
    }
}