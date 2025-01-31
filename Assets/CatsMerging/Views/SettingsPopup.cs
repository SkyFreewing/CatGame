using DG.Tweening;
using UnityEngine;

namespace CatMerge
{
    internal class SettingsPopup : GamePlayBlockerPopup
    {
        Tween _transformTween;

        public SettingsPopup() : base()
        {
            PrefabAssetPath = "Prefabs/SettingsPopup";
        }

        public override void Show()
        {
            base.Show();

            _transformTween = GameObject.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
        }

        public override void Hide()
        {
            base.Hide();

            _transformTween = GameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.05f)
                .OnComplete(() => GameObject.Destroy(this.GameObject));
        }
    }
}
