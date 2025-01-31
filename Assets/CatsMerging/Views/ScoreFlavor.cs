using CatMerge;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreFlavor : MonoBehaviour
{
    public TMP_Text ScoreText;
    public GameConfig GameConfig;

    Tween _transformTween;

    public void OnDestroy()
    {
        _transformTween.Kill();
        _transformTween = null;
    }

    public void SetScoreText(int Score)
    {
        ScoreText.SetText(Score.ToString());
        var ScoreColors = GameConfig.ScoreColors;
        var FlavorDuration = 0.4f;

        if (Score < 32)
        {
            ScoreText.color = ScoreColors[0];
        }
        else if (Score < 128)
        {
            ScoreText.color = ScoreColors[1];
            ScoreText.fontSize = 48;
            FlavorDuration = 0.45f;
        }
        else if (Score < 512) 
        { 
            ScoreText.color = ScoreColors[2];
            ScoreText.fontSize = 64;
            FlavorDuration = 0.5f;
        }
        else
        {
            ScoreText.color = ScoreColors[3];
            ScoreText.fontSize = 92;
            FlavorDuration = 0.7f;
        }

        _transformTween = gameObject.transform.DOMoveY(transform.localPosition.y + .5f, FlavorDuration)           
            .OnComplete(() => GameObject.Destroy(gameObject));
    }
}
