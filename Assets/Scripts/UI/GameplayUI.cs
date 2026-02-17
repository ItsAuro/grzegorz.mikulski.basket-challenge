using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayUI: MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _TMP_remainingTime;
    [SerializeField]
    TextMeshProUGUI _TMP_score;
    [SerializeField]
    TextMeshProUGUI _TMP_multiplier;

    const string TIME_LEFT = "Time {0}";
    const string SCORE = "Score {3}";
    const string MULTIPLIER = "Multiplier {0}x";


    void SetTimeLeft(int time)
    {
        _TMP_remainingTime.SetText(TIME_LEFT, time);

    }
    void SetScore(int score)
    {
        _TMP_score.SetText(SCORE, score);

    }
    void SetMultiplier(int multiplier)
    {
        _TMP_multiplier.SetText(MULTIPLIER, multiplier);

    }

    void Start()
    {
        SetTimeLeft(60);
        SetScore(0);
        SetMultiplier(1);

    }

}
