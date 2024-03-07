using TMPro;
using UnityEngine;
using DG.Tweening;

namespace CatMerge
{
    internal class Playable : MonoBehaviour, IMovable
    {
        [SerializeField] int _gradeIndex;
        [SerializeField] bool _willMerge;
        [SerializeField] Vector3 _position;

        Tween _transformTween;

        public TMP_Text GradeDisplay_Debug;

        public ITile Tile { get; set; }
        public int GradeIndex { get => _gradeIndex; set => _gradeIndex = value; }
        public bool WillMerge { get => _willMerge; set => _willMerge = value; }
        public Vector3 Position { get => _position; set => _position = value; }
      
        public void Update() 
        {
            GradeDisplay_Debug.text = GradeIndex.ToString();
        }

        void OnDestroy() 
        {
            _transformTween.Kill();
            _transformTween = null;
        }

        public void SetPosition(Vector3 newPosition, float duration)
        {
            _transformTween.Kill();
            Position = newPosition;

            var mergeOffset = _willMerge ? Vector3.back / 2 : Vector3.zero;
            newPosition -= mergeOffset;

            _transformTween = gameObject.transform.DOMove(newPosition, duration)
                .OnKill(() =>
                {
                    gameObject.transform.position = newPosition;
                    CheckForMerge();
                })
                .OnComplete(() => CheckForMerge());
        }

        void CheckForMerge() 
        {
            if (_willMerge)
                GameObject.Destroy(gameObject);          
        }

        public void SpawnAnimationPlay(Vector3 startScale, float duration) 
        {
            var finalScale = new Vector3(1, 1, 1);
            gameObject.transform.localScale = startScale;

            _transformTween = gameObject.transform.DOScale(finalScale, duration)
                .OnKill(() => gameObject.transform.localScale = finalScale);
        }
    }
}