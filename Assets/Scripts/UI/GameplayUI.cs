using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayUI: MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _TMP_Time;
    [SerializeField]
    TextMeshProUGUI _TMP_FireballMeter;
    [SerializeField]
    TextMeshProUGUI _TMP_FireballStatus;
    [SerializeField]
    TextMeshProUGUI _TMP_Score;

    const string _s_timeLeft       = "Time {0}";
    const string _s_fireballMeter  = "FireballMeter {0}/{1}";
    const string _s_fireballStatus = "FireballStatus {0}";
    const string _s_score          = "Score {0}";

    //time editor
    void SetTimeLeft(int time)
    {
        _TMP_Time.SetText(_s_timeLeft, time);
    }
    //score editor
    void SetScore(int score)
    {
        _TMP_Score.SetText(_s_score, score);

    }
    //fireball editors
    void SetFireballValue(int value)
    {
        _TMP_FireballMeter.SetText(_s_fireballMeter, value, GameConfig.FIREBALL_THRESHOLD);
    }
    void SetFireballEnable()
    {
        _TMP_FireballStatus.SetText(_s_fireballStatus, 1);
    }
    void SetFireballDisable()
    {
        _TMP_FireballStatus.SetText(_s_fireballStatus, 0);
    }

    void Start()
    {
        GameState gameState = GameplayController.Instance?.gameState;
        if (gameState == null) return;

        //fireball updates
        gameState.OnFireballValueChange += SetFireballValue;
        gameState.OnFireballEnable += SetFireballEnable;
        gameState.OnFireballDisable += SetFireballDisable;

        //score updates
        gameState.OnScoreChange += SetScore;

        //time updates
        gameState.OnRemainingTimeChange += SetTimeLeft;
    }

}
