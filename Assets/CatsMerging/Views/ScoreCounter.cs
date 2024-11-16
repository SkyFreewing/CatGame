using TMPro;
using UnityEngine;

namespace CatMerge
{
    internal class ScoreCounter : MonoBehaviour, IScoreCounter, IScoreChangeListener
    {
        public TMP_Text DisplayText;

        public ScoreCounter()
        {
            ScoreChangeEvent.AddListener(this);
        }

        void UpdateText(int newValue) 
        {
            DisplayText.text = newValue.ToString();
        }

        public void OnScoreChange(object e, int value)
        {
            UpdateText(value);
        }
    }
}
