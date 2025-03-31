using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace CatMerge
{
    internal class GameUISystem : IStartupSystem, IExecuteSystem, IResetGameListener
    {       
        const string scoreCounterPrefabPath = "Prefabs/ScoreCounter";
        const string settingsButtonPrefabPath = "Prefabs/SettingsButton";
        const string audioSliderPrefabPath = "Prefabs/AudioSlider";
        const string customCursorPrefabPath = "Prefabs/CustomCursor";

        IConfigCatalogue _configs;
        GameObject _gameCanvasGO;      
        RenderMode _startupRenderMode;
        bool _customCursorShow;

        public static List<IPopup> GamePopups = new List<IPopup>();
        public static Canvas GameCanvas;

        GameObject CustomCursor;

        public GameUISystem(IConfigCatalogue configs)
        {
            _configs = configs;
            _startupRenderMode = configs.GameUIConfig.GameCanvasRenderMode;
            _customCursorShow = configs.GameUIConfig.CustomCursorEnabled;

            ResetGameEvent.AddListener(this);
        }

        //Ugly custom cursor hack
        public void Execute()
        {
            if (_customCursorShow)
            {
                CustomCursor.transform.position = Input.mousePosition;
            }
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

            if (_customCursorShow)
            {
                var prefab = Resources.Load(customCursorPrefabPath);
                var instance = GameObject.Instantiate(prefab) as GameObject;
                instance.transform.SetParent(_gameCanvasGO.transform, false);

                CustomCursor = instance;
                Cursor.visible = false;
            }

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
