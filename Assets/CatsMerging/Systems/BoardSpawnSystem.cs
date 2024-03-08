using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;

namespace CatMerge
{
    internal class BoardSpawnSystem : IStartupSystem, IMoveCompletedListener
    {
        //Placeholder graphics to be replaced by visual systems
        const string texturePath = "Textures/PlaceholderPixel";
        const string texturePath2 = "Textures/Placeholder2";
        const string prefabPath = "Prefabs/Playable";

        Vector2 _boardSize;
        Vector3 _spawnStartScale;
        Vector3 _mergingScale;
        int _startPlayablesCount;
        int _spawnPlayablesCount;
        float _spawnScaleDuration;
        float _mergingScaleDuration;
        Color[] _gradeColors;
        

        GameObject _boardObject;
        List<BoardTile> _tileList = new List<BoardTile>();

        GameLostEvent _gameLostEvent;

        public BoardSpawnSystem(IConfigCatalogue configs)
        {
            _boardSize = configs.GameConfig.BoardSize;
            _startPlayablesCount = configs.GameConfig.StartPlayableCount;
            _spawnPlayablesCount = configs.GameConfig.SpawnPlayableCount;
            _spawnScaleDuration = configs.AnimConfig.SpawnScaleDuration;
            _mergingScaleDuration = configs.AnimConfig.MergingScaleDuration;
            _spawnStartScale = configs.AnimConfig.SpawnStartScale;
            _mergingScale = configs.AnimConfig.MergingScale;
            _gradeColors = configs.GameConfig.GradeColors;

            MoveCompletedEvent.AddListener(this);

            _gameLostEvent = new GameLostEvent();
        }
       
        public void Startup()
        {
            _boardObject = new GameObject("Board");

            for (int i = 1; i <= _boardSize.x; i++)
            {
                for (int j = 1; j <= _boardSize.y; j++) 
                {
                    var newTile = new GameObject("Tile");
                    newTile.transform.parent = _boardObject.transform;                    

                    var bt = newTile.AddComponent<BoardTile>();
                    MoveElementsSystem.Tiles.Add(bt);
                    bt.IsOccupied = false;
                    bt.SetPosition(new Vector3(j, i, 0));

                    var sr = newTile.AddComponent<SpriteRenderer>();
                    sr.sprite = Resources.Load<Sprite>(texturePath) as Sprite;

                    _tileList.Add(bt);
                }
            }

            for (int i = 0; i < _startPlayablesCount; i++) 
            {               
                var unoccupiedTiles = _tileList.Where(x => !x.IsOccupied).ToList();

                if (unoccupiedTiles.Any())
                {
                    var prefab = Resources.Load(prefabPath);
                    var newPlayable = GameObject.Instantiate(prefab) as GameObject;
                    var spawnTile = unoccupiedTiles[Random.Range(0, unoccupiedTiles.Count - 1)];

                    newPlayable.transform.parent = _boardObject.transform;
                    spawnTile.IsOccupied = true;

                    var pl = newPlayable.GetComponent<Playable>();
                    pl.SetPosition(spawnTile.transform.position + Vector3.back, 0f);
                    pl.SetGrade(0, _gradeColors, _mergingScale, _mergingScaleDuration);
                    pl.SpawnAnimationPlay(_spawnStartScale, _spawnScaleDuration);
                    MoveElementsSystem.Movables.Add(pl);
                    pl.Tile = spawnTile;
                }
            }         
        }

        public void OnMoveCompleted(object e, bool flag)
        {
            for (int i = 0; i < _spawnPlayablesCount; i++)
            {               
                var unoccupiedTiles = _tileList.Where(x => !x.IsOccupied).ToList();

                if (unoccupiedTiles.Any())
                {
                    var prefab = Resources.Load(prefabPath);
                    var newPlayable = GameObject.Instantiate(prefab) as GameObject;
                    var spawnTile = unoccupiedTiles[Random.Range(0, unoccupiedTiles.Count - 1)];

                    newPlayable.transform.parent = _boardObject.transform;
                    spawnTile.IsOccupied = true;

                    var pl = newPlayable.GetComponent<Playable>();
                    pl.SetPosition(spawnTile.transform.position + Vector3.back, 0f);
                    pl.SetGrade(0, _gradeColors, _mergingScale, _mergingScaleDuration);
                    pl.SpawnAnimationPlay(_spawnStartScale, _spawnScaleDuration);
                    MoveElementsSystem.Movables.Add(pl);
                    pl.Tile = spawnTile;
                }
                else 
                {
                    _gameLostEvent.OnGameLost();
                }
            }
        }
    }
}