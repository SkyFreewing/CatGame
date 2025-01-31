using TMPro;
using UnityEngine;

namespace CatMerge 
{
    public class EndOfGameScoreDisplay : MonoBehaviour
    {
        public int Score;
        public TMP_Text ScoreText;

        void Start()
        {
            ScoreText.text = Score.ToString();
        }
    }
}
