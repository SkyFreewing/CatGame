using UnityEngine;

namespace CatMerge
{
    internal interface IPopup
    {
        public GameObject GameObject { get; set; }
        public string PrefabAssetPath { get; set; }
        public void Show();
        public void Hide();
    }
}
