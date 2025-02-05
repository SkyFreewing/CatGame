using UnityEngine;
using UnityEngine.UI;

namespace CatMerge
{
    public class BackButton : MonoBehaviour
    {
        public Button Button;

        public void Start()
        {         
            Button.onClick.AddListener(ClosePopup);
        }

        public void ClosePopup() 
        {
            var popup = GameUISystem.GamePopups[0];
            GameUISystem.RemoveGamePopup(popup);

            var sound = SoundSystem.Instance;
            sound.PlayUISound(false);
            sound.DampenBGMVolume(false);
        }
    }
}
