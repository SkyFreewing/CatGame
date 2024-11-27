using UnityEngine;

namespace CatMerge
{
    public interface IGameUIConfig
    {
        RenderMode GameCanvasRenderMode { get; }  
        bool ScoreCounterEnabled { get; }
    }
}