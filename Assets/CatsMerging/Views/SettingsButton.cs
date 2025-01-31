using UnityEngine;
using UnityEngine.UI;

namespace CatMerge
{
    public class SettingsButton : MonoBehaviour
    {
        public Button Button;

        public void Start()
        {           
            Button.onClick.AddListener(OpenSettingsMenu);
        }

        public void OpenSettingsMenu() 
        {
            var SettingsMenu = new SettingsPopup();
            GameUISystem.CreateGamePopup(SettingsMenu);
        }
    }
}
