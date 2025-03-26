using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatMerge
{
    internal class GameUISystem : IStartupSystem, IResetGameListener
    {
        const string scoreCounterPrefabPath = "Prefabs/ScoreCounter";
        const string settingsButtonPrefabPath = "Prefabs/SettingsButton";
        const string audioSliderPrefabPath = "Prefabs/AudioSlider";

        IConfigCatalogue _configs;
        GameObject _gameCanvasGO;      
        RenderMode _startupRenderMode;

        public static List<IPopup> GamePopups = new List<IPopup>();
        public static Canvas GameCanvas;

        public GameUISystem(IConfigCatalogue configs)
        {
            _configs = configs;
            _startupRenderMode = configs.GameUIConfig.GameCanvasRenderMode;

            ResetGameEvent.AddListener(this);
        }

        public void Startup()
        {
            _gameCanvasGO = new GameObject("Game Canvas");
            GameCanvas = _gameCanvasGO.AddComponent<Canvas>();
            _gameCanvasGO.AddComponent<GraphicRaycaster>();

            GameCanvas.sortingLayerID = 5;
            GameCanvas.sortingOrder = 1;
            GameCanvas.renderMode = _startupRenderMode;

            var prefabPaths = new List<string>();

            if (_configs.GameUIConfig.ScoreCounterEnabled)
            {
                prefabPaths.Add(scoreCounterPrefabPath);
            }

            prefabPaths.Add(settingsButtonPrefabPath);
            prefabPaths.Add(audioSliderPrefabPath);

            foreach (var prefabPath in prefabPaths)
            {
                var prefab = Resources.Load(prefabPath);
                var instance = GameObject.Instantiate(prefab) as GameObject;
                instance.transform.SetParent(_gameCanvasGO.transform, false);
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
            }
        }

        void RemoveAllPopups() 
        {
            var AllOpenPopups = GamePopups.ToArray();

            foreach (var pop in AllOpenPopups) 
            {
                RemoveGamePopup(pop);
            }
        }

        public void OnResetGame(object e, bool flag)
        {
            RemoveAllPopups();
        }
    }
}
