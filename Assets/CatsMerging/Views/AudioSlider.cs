using UnityEngine;
using UnityEngine.UI;

namespace CatMerge
{
    public class AudioSlider : MonoBehaviour
    {
        public Button Button;
        public GameObject Slider;

        public void Start()
        {
            Button.onClick.AddListener(ToggleAudioSlider);
            var audioSlider = Slider.GetComponentInChildren<Slider>();
            audioSlider.onValueChanged.AddListener(UpdateAudioVolume);
        }

        public void ToggleAudioSlider()
        {
            Slider.SetActive(!Slider.activeSelf);
        }

        public void UpdateAudioVolume(float volume)
        {
            var sound = SoundSystem.Instance;
            sound.SetVolume(volume);
        }
    }
}
