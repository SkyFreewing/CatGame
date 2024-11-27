using TMPro;
using UnityEngine;

namespace CatMerge
{
    internal class ScoreCounter : MonoBehaviour, IScoreCounter, IScoreChangeListener
    {
        public TMP_Text DisplayText;

        void Awake()
        {
            ScoreChangeEvent.AddListener(this);
            UpdateText(0);
        }

        void UpdateText(int newValue) 
        {
            DisplayText.SetText(newValue.ToString());
        }

        public void OnScoreChange(object e, int value)
        {
            UpdateText(value);
        }
    }
}
