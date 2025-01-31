using DG.Tweening;
using UnityEngine;

namespace CatMerge
{
    internal class SettingsPopup : GamePlayBlockerPopup
    {
        public SettingsPopup() : base()
        {
            PrefabAssetPath = "Prefabs/SettingsPopup";
        }

        public override void Show()
        {
            base.Show();

            GameObject.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
        }

        public override void Hide()
        {
            base.Hide();

            GameObject.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
        }
    }
}
