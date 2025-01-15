using System.Collections.Generic;
using UnityEngine;

namespace CatMerge
{
    internal class GameUISystem : IStartupSystem
    {
        const string scoreCounterPrefabPath = "Prefabs/ScoreCounter";

        IConfigCatalogue _configs;
        GameObject _gameCanvasGO;      
        RenderMode _startupRenderMode;

        public static List<IPopup> GamePopups = new List<IPopup>();
        public static Canvas GameCanvas;

        public GameUISystem(IConfigCatalogue configs)
        {
            _configs = configs;
            _startupRenderMode = configs.GameUIConfig.GameCanvasRenderMode;
        }

        public void Startup()
        {
            _gameCanvasGO = new GameObject("Game Canvas");
            GameCanvas = _gameCanvasGO.AddComponent<Canvas>();
            GameCanvas.renderMode = _startupRenderMode;

            if (_configs.GameUIConfig.ScoreCounterEnabled) 
            {
                var scoreCounterPrefab = Resources.Load(scoreCounterPrefabPath);
                var scoreCounter = GameObject.Instantiate(scoreCounterPrefab) as GameObject;
                scoreCounter.transform.SetParent(_gameCanvasGO.transform, false);
            }
        }

        public static void CreateGamePopup(IPopup popup)
        {
            if (!GamePopups.Contains(popup))
            {
                GamePopups.Add(popup);

                var prefab = Resources.Load(popup.PrefabAssetPath);
                var popupGO = GameObject.Instantiate(prefab, GameCanvas.transform, false) as GameObject;
                popup.GameObject = popupGO;
                popup.Show();
            }
        }

        public static void RemoveGamePopup(IPopup popup)
        {
            if (GamePopups.Remove(popup))
            {
                popup.Hide();
                GameObject.Destroy(popup.GameObject);
            }
        }
    }
}
