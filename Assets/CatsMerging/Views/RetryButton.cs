using UnityEngine;
using UnityEngine.UI;

namespace CatMerge
{
    public class RetryButton : MonoBehaviour
    {
        public Button Button;
        ResetGameEvent _resetGameEvent; 

        public void Start()
        {
            _resetGameEvent = new ResetGameEvent();

            Button.onClick.AddListener(ResetGame);
        }

        public void ResetGame()
        {
            _resetGameEvent.OnResetGame(true);

            var sound = SoundSystem.Instance;
            sound.PlayUISound(true);
            sound.DampenBGMVolume(false);
        }
    }
}
