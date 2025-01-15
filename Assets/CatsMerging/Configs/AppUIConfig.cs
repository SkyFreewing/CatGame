using UnityEngine;

namespace CatMerge
{
    [CreateAssetMenu(fileName = "AppUIConfig", menuName = "ScriptableObjects/AppUIConfig", order = 6)]
    internal class AppUIConfig : ScriptableObject, IAppUIConfig
    {
        [SerializeField] RenderMode _appCanvasRenderMode;

        public RenderMode AppCanvasRenderMode => _appCanvasRenderMode;
    }
}
