using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CatMerge
{
    internal class BoardSpawnSystem : IStartupSystem, IMoveCompletedListener, IResetGameListener
    {
        //Placeholder graphics
        const string texturePath = "Textures/PlaceholderPixel";
        const string prefabPath = "Prefabs/Playable";

        Vector2 _boardSize;
        Vector3 _spawnStartScale;
        Vector3 _mergingScale;
        int _startPlayablesCount;
        int _spawnPlayablesCount;
        float _spawnScaleDuration;
        float _mergingScaleDuration;
        Sprite[] _gradeSprites;
       

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
            _gradeSprites = configs.GameConfig.GradeSprites;

            MoveCompletedEvent.AddListener(this);
            ResetGameEvent.AddListener(this);

            _gameLostEvent = new GameLostEvent();
        }
       
        public void Startup()
        {       
            //Spawn the board
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

            SpawnStartMovables();
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
                    pl.SetGrade(0, _gradeSprites, _mergingScale, _mergingScaleDuration);
                    //Check here if the animation should really be controlled by the system and not only the view 2
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

        public void SpawnStartMovables() 
        {
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
                    pl.SetGrade(0, _gradeSprites, _mergingScale, _mergingScaleDuration);
                    //Check here if the animation should really be controlled by the system and not only the view 1
                    pl.SpawnAnimationPlay(_spawnStartScale, _spawnScaleDuration);
                    MoveElementsSystem.Movables.Add(pl);
                    pl.Tile = spawnTile;
                }
            }
        }

        public void OnResetGame(object e, bool flag)
        {
            foreach(var mov in MoveElementsSystem.Movables) 
            {
                mov.Tile = null;
                mov.Kill();
            }

            MoveElementsSystem.Movables.Clear();

            foreach (var tile in MoveElementsSystem.Tiles) 
            {
                tile.IsOccupied = false;
            }
            
            SpawnStartMovables();
        }
    }
}