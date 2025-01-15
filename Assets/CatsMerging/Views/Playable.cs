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
        Sequence _upgradeSequence;

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
            _upgradeSequence.Kill();
            _transformTween = null;
            _upgradeSequence = null;
        }

        public void SetGrade(int newGrade, Color[] gradeColors, Vector3 targetScale, float scaleDuration) 
        {
            _gradeIndex = newGrade;

            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            spriteRenderer.GetPropertyBlock(propertyBlock);
           
            _upgradeSequence.Kill();
            _upgradeSequence = DOTween.Sequence();

            var scale = gameObject.transform.localScale;

            _upgradeSequence.Append(gameObject.transform.DOScale(targetScale, scaleDuration))
                            .Append(gameObject.transform.DOScale(scale, scaleDuration))
                            .OnKill(() => gameObject.transform.localScale = scale)
                            .Play();

            if (gradeColors.Length > newGrade)
                propertyBlock.SetColor("_Color", gradeColors[newGrade]);
            else
                propertyBlock.SetColor("_Color", Color.magenta);
                     
            spriteRenderer.SetPropertyBlock(propertyBlock);
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
            _transformTween.Kill();
            var finalScale = new Vector3(1, 1, 1);
            gameObject.transform.localScale = startScale;

            _transformTween = gameObject.transform.DOScale(finalScale, duration)
                .OnKill(() => gameObject.transform.localScale = finalScale);
        }
    }
}