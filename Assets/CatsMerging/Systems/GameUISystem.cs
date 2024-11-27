using UnityEngine;

namespace CatMerge
{
    internal class GameUISystem : IStartupSystem
    {
        const string scoreCounterPrefabPath = "Prefabs/ScoreCounter";

        IConfigCatalogue _configs;
        Canvas _gameCanvas;
        GameObject _gameCanvasGO;
        RenderMode _startupRenderMode;

        public GameUISystem(IConfigCatalogue configs)
        {
            _configs = configs;
            _startupRenderMode = configs.GameUIConfig.GameCanvasRenderMode;
        }

        public void Startup()
        {
            _gameCanvasGO = new GameObject("Game Canvas");
            _gameCanvas = _gameCanvasGO.AddComponent<Canvas>();
            _gameCanvas.renderMode = _startupRenderMode;

            if (_configs.GameUIConfig.ScoreCounterEnabled) 
            {
                var scoreCounterPrefab = Resources.Load(scoreCounterPrefabPath);
                var scoreCounter = GameObject.Instantiate(scoreCounterPrefab) as GameObject;
                scoreCounter.transform.SetParent(_gameCanvasGO.transform, false);
            }
        }
    }
}
