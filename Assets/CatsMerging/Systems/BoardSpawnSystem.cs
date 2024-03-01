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
        const string texturePath = "Textures/Placeholder";
        const string texturePath2 = "Textures/Placeholder 2";
        const string prefabPath = "Prefabs/Playable";

        Vector2 _boardSize;
        int _startPlayablesCount;
        int _spawnPlayablesCount;

        GameObject _boardObject;
        List<BoardTile> _tileList = new List<BoardTile>();

        public BoardSpawnSystem(IConfigCatalogue configs)
        {
            _boardSize = configs.GameConfig.BoardSize;
            _startPlayablesCount = configs.GameConfig.StartPlayableCount;
            _spawnPlayablesCount = configs.GameConfig.SpawnPlayableCount;

            MoveCompletedEvent.AddListener(this);
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
                var prefab = Resources.Load(prefabPath);
                var newPlayable = GameObject.Instantiate(prefab) as GameObject;
                var unoccupiedTiles = _tileList.Where(x => !x.IsOccupied).ToList();
                var spawnTile = unoccupiedTiles[Random.Range(0, unoccupiedTiles.Count - 1)];

                newPlayable.transform.parent = _boardObject.transform; 
                spawnTile.IsOccupied = true;

                var pl = newPlayable.GetComponent<Playable>();
                pl.SetPosition(spawnTile.transform.position + Vector3.back, 0f);
                MoveElementsSystem.Movables.Add(pl);
                pl.Tile = spawnTile;
            }         
        }

        public void OnMoveCompleted(object e, bool flag)
        {
            for (int i = 0; i < _spawnPlayablesCount; i++)
            {
                var prefab = Resources.Load(prefabPath);
                var newPlayable = GameObject.Instantiate(prefab) as GameObject;
                var unoccupiedTiles = _tileList.Where(x => !x.IsOccupied).ToList();
                var spawnTile = unoccupiedTiles[Random.Range(0, unoccupiedTiles.Count - 1)];

                newPlayable.transform.parent = _boardObject.transform;
                spawnTile.IsOccupied = true;

                var pl = newPlayable.GetComponent<Playable>();
                pl.SetPosition(spawnTile.transform.position + Vector3.back, 0f);
                MoveElementsSystem.Movables.Add(pl);
                pl.Tile = spawnTile;
            }
        }
    }
}