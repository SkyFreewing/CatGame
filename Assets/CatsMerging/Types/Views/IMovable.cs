using UnityEngine;

namespace CatMerge
{
    internal interface IMovable 
    {
        ITile Tile { get; set; }
        int GradeIndex { get; set; }
        bool WillMerge { get; set; }
        Vector3 Position { get; set; }
        void SetPosition(Vector3 newPosition, float duration);
        void SetGrade(int newGrade, Color[] gradeColors, Vector3 mergingScale, float mergingScaleDuration);
    }
}