using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CatMerge;

namespace CatMerge
{
    internal class MoveElements : MonoBehaviour, IInputChangedListener
    {
        public List<IMovable> Movables =  new List<IMovable>();
        public List<ITile> Tiles = new List<ITile>();

        float _movementDuration;

        void OnEnable () => InputChangedEvent.AddListener(this);
        void OnDisable() => InputChangedEvent.RemoveListener(this);

        void Start() 
        {
            _movementDuration = AppController.GlobalConfigCatalogue.AnimConfig.MovableAnimationDuration;
        }
        
        public void OnInputChanged(object e, Vector2 input)
        {
            if (Movables.Count <= 0 || Tiles.Count <= 0) return;

            var xValue = Mathf.Abs(input.x);
            var yValue = Mathf.Abs(input.y);
            var inputIsX = xValue > yValue;
            var direction = inputIsX ? Mathf.Sign(input.x) : Mathf.Sign(input.y);
            var boardSpan = Mathf.Sqrt(Tiles.Count);
            var movableTargetTiles = new Dictionary<IMovable, ITile>();
            var mergeTargets = new List<IMovable>();

            foreach (var mov in Movables) 
            {
                var obstacleMovables = new List<IMovable>();

                var position = mov.Position;
                var movableAxis = inputIsX ? position.y : position.x;
                var movablePositionOnAxis = inputIsX ? position.x : position.y;

                int equalGradesCount = 0;
                IMovable mergeTarget = null;               

                if (direction > 0)
                {
                    obstacleMovables = Movables.Where(m => inputIsX ? m.Position.x > movablePositionOnAxis
                                                            && m.Position.y == movableAxis
                                                            : m.Position.y > movablePositionOnAxis
                                                            && m.Position.x == movableAxis)
                                                            .OrderBy(m => inputIsX ? m.Position.x
                                                            : m.Position.y)
                                                            .ToList<IMovable>();
                }
                else
                {
                    obstacleMovables = Movables.Where(m => inputIsX ? m.Position.x < movablePositionOnAxis
                                                            && m.Position.y == movableAxis
                                                            : m.Position.y < movablePositionOnAxis
                                                            && m.Position.x == movableAxis)
                                                            .OrderByDescending(m => inputIsX ? m.Position.x
                                                            : m.Position.y)
                                                            .ToList<IMovable>();                  
                }

                for (var i = 0; i < obstacleMovables.Count; i++)
                {
                    if (obstacleMovables[i].GradeIndex == mov.GradeIndex)
                        equalGradesCount++;
                    else
                        break;
                }

                if (equalGradesCount > 0 && equalGradesCount % 2 != 0)
                    mergeTarget = obstacleMovables[0];

                if (mergeTarget != null)
                {
                    mov.WillMerge = true;
                    mergeTargets.Add(mergeTarget);
                }
            }

            foreach (var mov in Movables) 
            {
                var obstacleMovables = new List<IMovable>();
                var targetTiles = new List<ITile>();

                var position = mov.Position;
                var movableAxis = inputIsX ? position.y : position.x;
                var movablePositionOnAxis = inputIsX ? position.x : position.y;
                
                if (direction > 0)
                {
                    targetTiles = Tiles.Where(t => inputIsX ? t.Position.x > movablePositionOnAxis
                                                            && t.Position.y == movableAxis
                                                            : t.Position.y > movablePositionOnAxis
                                                            && t.Position.x == movableAxis)
                                                            .ToList<ITile>();
                    obstacleMovables = Movables.Where(m => inputIsX ? m.Position.x > movablePositionOnAxis
                                                            && m.Position.y == movableAxis
                                                            : m.Position.y > movablePositionOnAxis
                                                            && m.Position.x == movableAxis)
                                                            .ToList<IMovable>();                    
                }
                else
                {
                    targetTiles = Tiles.Where(t => inputIsX ? t.Position.x < movablePositionOnAxis
                                                            && t.Position.y == movableAxis
                                                            : t.Position.y < movablePositionOnAxis
                                                            && t.Position.x == movableAxis)
                                                            .ToList<ITile>();
                    obstacleMovables = Movables.Where(m => inputIsX ? m.Position.x < movablePositionOnAxis
                                                            && m.Position.y == movableAxis
                                                            : m.Position.y < movablePositionOnAxis
                                                            && m.Position.x == movableAxis)
                                                            .ToList<IMovable>();
                }

                var boardSteps = direction > 0 ? boardSpan - movablePositionOnAxis : movablePositionOnAxis - 1;
                boardSteps -= obstacleMovables.Count;

                if (mov.WillMerge)
                    boardSteps++;

                foreach (var obstacle in obstacleMovables.Where(obs => obs.WillMerge)) 
                {
                    boardSteps++;
                }
               
                foreach (var tile in targetTiles)
                {
                    var tilePosition = tile.Position;
                    var tilePositionOnAxis = inputIsX ? tilePosition.x : tilePosition.y;

                    if (tilePositionOnAxis == (movablePositionOnAxis + boardSteps * direction))
                        movableTargetTiles.Add(mov, tile);
                }

            }

            foreach (var targetPair in movableTargetTiles) 
            {
                //TODO: Extract merge mechanic to Views only set bools here

                var movable = targetPair.Key;
                var targetTile = targetPair.Value;

                movable.Tile.IsOccupied = false;
                movable.Tile = targetTile;
                targetTile.IsOccupied = true;

                movable.SetPosition(targetTile.Position + Vector3.back, _movementDuration);

                if (movable.WillMerge)              
                    Movables.Remove(movable);                
            }

            foreach (var movable in mergeTargets) 
            {
                movable.GradeIndex++;
            }
        }
    }
}
