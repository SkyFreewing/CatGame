using System.Collections.Generic;
using UnityEngine;

namespace CatMerge
{
    internal class AppUISystem : IStartupSystem
    {     
        GameObject _appCanvasGO;    
        RenderMode _startupRenderMode;

        public static Canvas AppCanvas;
        public static List<IPopup> AppPopups = new List<IPopup>();

        public AppUISystem(IConfigCatalogue configs)
        {
            _startupRenderMode = configs.AppUIConfig.AppCanvasRenderMode;
        }

        public void Startup()
        {
            _appCanvasGO = new GameObject("App Canvas");
            AppCanvas = _appCanvasGO.AddComponent<Canvas>();
            AppCanvas.renderMode = _startupRenderMode;
        }

        public static void CreateAppPopup(IPopup popup) 
        {
            if (!AppPopups.Contains(popup))
            {
                AppPopups.Add(popup);

                var prefab = Resources.Load(popup.PrefabAssetPath);
                var popupGO = GameObject.Instantiate(prefab, AppCanvas.transform, false) as GameObject;
                popup.GameObject = popupGO;
                popup.Show();
            }
        }

        public static void RemoveAppPopup(IPopup popup) 
        {
            if (AppPopups.Remove(popup)) 
            {
                popup.Hide();
                GameObject.Destroy(popup.GameObject);
            }
        }
    }
}
