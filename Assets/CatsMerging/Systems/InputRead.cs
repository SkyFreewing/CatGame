using System;
using UnityEngine;

namespace CatMerge
{
    internal class InputRead : IExecuteSystem
    {
        float _inputCorrectnessThreshold;
        float _inputMinimumDistance;

        Vector2 _lastPosition;
        Vector2 InputVector;

        InputChangedEvent _inputChangedEvent;

        public InputRead(IConfigCatalogue configs)
        {
            _inputCorrectnessThreshold = configs.InputConfig.SwipeCorrectnessThreshold;
            _inputMinimumDistance = configs.InputConfig.SwipeMinimumDistance;

            _inputChangedEvent = new InputChangedEvent();
        }

        public void Execute()
        {     
            if (Input.GetMouseButton(0))
            {
                var currentPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                
                if (_lastPosition == Vector2.zero)
                {
                    _lastPosition = currentPosition;
                    return;                
                }  
                
                var distanceVector = currentPosition - _lastPosition;
                _lastPosition = currentPosition;

                Vector2 movementDirection = Vector2.zero;

                if (distanceVector.magnitude > _inputMinimumDistance)
                {
                    if (Vector2.Dot(distanceVector.normalized, Vector2.up) > _inputCorrectnessThreshold)
                            movementDirection = Vector2.up;
                    else if (Vector2.Dot(distanceVector.normalized, Vector2.down) > _inputCorrectnessThreshold)
                            movementDirection = Vector2.down;
                    else if (Vector2.Dot(distanceVector.normalized, Vector2.left) > _inputCorrectnessThreshold)
                            movementDirection = Vector2.left;
                    else if (Vector2.Dot(distanceVector.normalized, Vector2.right) > _inputCorrectnessThreshold)
                            movementDirection = Vector2.right;

                    if (InputVector != movementDirection && movementDirection != Vector2.zero)
                        InputDirectionChanged(movementDirection);              
                }                 
            }

            if (Input.GetMouseButtonUp(0))
            {
                _lastPosition = Vector2.zero;
                InputVector = Vector2.zero;
            }
        }

        public void InputDirectionChanged(Vector2 movementDirection) 
        {
            InputVector = movementDirection;
            _inputChangedEvent.OnInputChanged(InputVector);
        }
    }
}
