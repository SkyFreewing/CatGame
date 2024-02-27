using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace CatMerge
{
    internal class BoardSpawn : IStartupSystem
    {
        //Placeholder graphics to be replaced by visual systems
        const string texturePath = "Textures/Placeholder";
        const string texturePath2 = "Textures/Placeholder 2";
        const string prefabPath = "Prefabs/Playable";

        Vector2 boardSize;
        int startPlayables;

        public BoardSpawn(IConfigCatalogue configs)
        {
            boardSize = configs.GameConfig.BoardSize;
            startPlayables = configs.GameConfig.StartPlayableCount;
        }

        public void Startup()
        {
            var boardObject = new GameObject("Board");
            var tileList = new List<BoardTile>();
            var moveElements = boardObject.AddComponent<MoveElements>();

            
            for (int i = 1; i <= boardSize.x; i++)
            {
                for (int j = 1; j <= boardSize.y; j++) 
                {
                    var newTile = new GameObject("Tile");
                    newTile.transform.parent = boardObject.transform;                    

                    var bt = newTile.AddComponent<BoardTile>();
                    moveElements.Tiles.Add(bt);
                    bt.IsOccupied = false;
                    bt.SetPosition(new Vector3(j, i, 0));

                    var sr = newTile.AddComponent<SpriteRenderer>();
                    sr.sprite = Resources.Load<Sprite>(texturePath) as Sprite;

                    tileList.Add(bt);
                }
            }

            for (int i = 0; i < startPlayables; i++) 
            {
                var prefab = Resources.Load(prefabPath);
                var newPlayable = GameObject.Instantiate(prefab) as GameObject;
                var unoccupiedTiles = tileList.Where(x => x.IsOccupied == false).ToList();
                var spawnTile = unoccupiedTiles[Random.Range(0, unoccupiedTiles.Count - 1)];

                newPlayable.transform.parent = boardObject.transform; 
                spawnTile.IsOccupied = true;

                var pl = newPlayable.GetComponent<Playable>();
                pl.SetPosition(spawnTile.transform.position + Vector3.back, 0f);
                moveElements.Movables.Add(pl);
                pl.Tile = spawnTile;
            }         
        }
    }
}