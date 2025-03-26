using UnityEngine;
using UnityEngine.UI;

namespace CatMerge
{
    public class AudioSlider : MonoBehaviour, IInputChangedListener
    {
        public Button Button;
        public GameObject Slider;

        public void Start()
        {
            Button.onClick.AddListener(ToggleAudioSlider);
            Slider.GetComponent<Slider>().onValueChanged.AddListener(UpdateAudioVolume);
            InputChangedEvent.AddListener(this);
        }

        public void OnDestroy()
        {
            InputChangedEvent.RemoveListener(this);
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

        public void OnInputChanged(object sender, Vector2 vector)
        {
            Slider.SetActive(false);
        }
    }
}
