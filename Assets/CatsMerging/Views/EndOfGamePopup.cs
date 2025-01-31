using DG.Tweening;
using UnityEngine;

namespace CatMerge
{
    internal class EndOfGamePopup : GamePlayBlockerPopup
    {
        public int Score;

        EndOfGameScoreDisplay _scoreDisplay;

        public EndOfGamePopup() : base()
        {
            PrefabAssetPath = "Prefabs/EndgamePopup";
        }

        public override void Show()
        {
            base.Show();
            _scoreDisplay = GameObject.GetComponentInChildren<EndOfGameScoreDisplay>();
            _scoreDisplay.Score = Score;

            GameObject.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
        }

        public override void Hide()
        {
            base.Hide();
            _scoreDisplay.Score = 0;
            
            GameObject.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
        }
    }
}
