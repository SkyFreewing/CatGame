using UnityEngine;

namespace CatMerge
{
    internal interface IMovable 
    {
        GameObject GameObject { get; }
        ITile Tile { get; set; }
        int GradeIndex { get; set; }
        bool WillMerge { get; set; }
        Vector3 Position { get; set; }
        void SetPosition(Vector3 newPosition, float duration);
    }
}