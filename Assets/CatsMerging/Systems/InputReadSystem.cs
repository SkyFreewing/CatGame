using UnityEngine;

namespace CatMerge
{
    internal class InputReadSystem : IExecuteSystem
    {
        float _inputCorrectnessThreshold;
        float _inputMinimumDistance;

        Vector2 _lastPosition;
        Vector2 InputVector;

        InputChangedEvent _inputChangedEvent;

        public InputReadSystem(IConfigCatalogue configs)
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

#if UNITY_EDITOR || UNITY_WEBGL
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) 
            {
                InputDirectionChanged(Vector2.up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                InputDirectionChanged(Vector2.down);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                InputDirectionChanged(Vector2.right);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                InputDirectionChanged(Vector2.left);
            }
#endif
        }

        public void InputDirectionChanged(Vector2 movementDirection) 
        {
            InputVector = movementDirection;
            _inputChangedEvent.OnInputChanged(InputVector);
        }
    }
}
