using TMPro;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UIElements;

namespace CatMerge
{
    internal class Playable : MonoBehaviour, IMovable
    {
        [SerializeField] int _gradeIndex;
        [SerializeField] bool _willMerge;
        [SerializeField] Vector3 _position;
        [SerializeField] ParticleSystem _particleSystem;
        [SerializeField] GameObject[] _backgroundEffects;

        Tween _transformTween;
        Sequence _upgradeSequence;
        const string _scoreFlavorPrefabPath = "Prefabs/ScoreFlavor";

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
            _transformTween?.Kill();
            _upgradeSequence?.Kill();
            _transformTween = null;
            _upgradeSequence = null;
        }

        public void Kill() 
        {
            GameObject.Destroy(gameObject);
        }

        public void SetGrade(int newGrade, Sprite[] gradeSprites, Vector3 targetScale, float scaleDuration) 
        {
            _gradeIndex = newGrade;

            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            spriteRenderer.GetPropertyBlock(propertyBlock);
           
            _upgradeSequence.Kill();
            _upgradeSequence = DOTween.Sequence();           

            var scale = transform.localScale;

            _upgradeSequence.Append(gameObject.transform.DOScale(targetScale, scaleDuration))
                            .Append(gameObject.transform.DOScale(scale, scaleDuration))
                            .OnKill(() => gameObject.transform.localScale = scale)
                            .Play();          

            if (gradeSprites.Length > newGrade)
            {
                if (newGrade < 12)
                    spriteRenderer.sprite = gradeSprites[newGrade];
                else
                    spriteRenderer.sprite = gradeSprites[12];

                if (newGrade > 0)
                {
                    var scoreFlavorPrefab = Resources.Load(_scoreFlavorPrefabPath);
                    var scoreFlavor = GameObject.Instantiate(scoreFlavorPrefab) as GameObject;
                    scoreFlavor.transform.Translate(transform.localPosition);
                    scoreFlavor.GetComponent<ScoreFlavor>().SetScoreText((int)Math.Pow(2, newGrade));

                    scoreFlavor.transform.SetParent(GameObject.Find("FlavorCanvas").transform);
                }
                if (newGrade > 2)
                    _particleSystem.Play();
                if(newGrade > 5)
                    _backgroundEffects[0].SetActive(true);
                if(newGrade > 7)
                    _backgroundEffects[1].SetActive(true);
                if(newGrade > 9)
                    _backgroundEffects[2].SetActive(true);             
            }
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
                .OnComplete(() => 
                { 
                    CheckForMerge();
                });
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