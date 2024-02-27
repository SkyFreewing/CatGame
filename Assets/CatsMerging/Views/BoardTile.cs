using UnityEngine;

namespace CatMerge
{
    internal class BoardTile : MonoBehaviour, ITile
    {
        [SerializeField] bool _isOccupied;
        [SerializeField] Vector3 _position;

        public bool IsOccupied { get => _isOccupied; set => _isOccupied = value; }
        public Vector3 Position { get => _position; set => _position = value; }

        public void SetPosition(Vector3 newPosition) 
        {
            Position = newPosition;
            gameObject.transform.position = newPosition;
        }
    }
}