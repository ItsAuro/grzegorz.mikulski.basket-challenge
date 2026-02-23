using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI: MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _TMP_Time;
    [SerializeField]
    TextMeshProUGUI _TMP_FireballMeter;
    [SerializeField]
    TextMeshProUGUI _TMP_FireballStatus;
    [SerializeField]
    TextMeshProUGUI _TMP_FireballMultiplier;
    [SerializeField]
    TextMeshProUGUI _TMP_Score;
    [SerializeField]
    TextMeshProUGUI _TMP_PowerMeter;
    [SerializeField]
    Slider _Slider_PowerMeter;
    [SerializeField]
    Slider _Slider_FireBallMeter;
    [SerializeField]
    InputHandler _inputHandler;

    const string _s_timeLeft           = "Time\n{0}";
    const string _s_fireballMeter      = "FireballMeter {0}/{1}";
    const string _s_fireballStatus     = "FireballStatus {0}";
    const string _s_score              = "Score {0:D8}";
    const string _s_powerMeter         = "Power {0}/{1}";
    const string _s_fireballMultiplier = "x{0}";

    //time editor
    void SetTimeLeft(int time)
    {
        _TMP_Time.SetText(_s_timeLeft, time);
    }
    //score editor
    void SetScore(int score)
    {
        //_TMP_Score.SetText(_s_score, score);
        _TMP_Score.text = string.Format(_s_score, score);

    }
    //fireball editors
    void SetFireballValue(int value)
    {
        _TMP_FireballMeter.SetText(_s_fireballMeter, value, GameConfig.FIREBALL_THRESHOLD);
        _Slider_FireBallMeter.SetValueWithoutNotify((float)value / GameConfig.FIREBALL_THRESHOLD);
    }
    void SetFireballEnable()
    {
        _TMP_FireballStatus.SetText(_s_fireballStatus, 1);
        SetFireballMultiplierVisibility(true);
    }
    void SetFireballDisable()
    {
        _TMP_FireballStatus.SetText(_s_fireballStatus, 0);
        SetFireballMultiplierVisibility(false);
    }
    void SetFireballMultiplierValue(int value)
    {
        _TMP_FireballMultiplier.SetText(_s_fireballMultiplier, value);
    }
    void SetFireballMultiplierVisibility(bool visibility)
    {
        _TMP_FireballMultiplier.enabled = visibility;
    }
    //power editors
    void SetPowerValue(float value)
    {
        _TMP_PowerMeter.SetText(_s_powerMeter, value, 1);
        _Slider_PowerMeter.SetValueWithoutNotify(value);
    }
    void ResetPowerValue()
    {
        _TMP_PowerMeter.SetText(_s_powerMeter, 0, 1);
        _Slider_PowerMeter.SetValueWithoutNotify(0);
    }

    void Start()
    {
        if (_inputHandler != null)
        {
            SetPowerValue(0);

            _inputHandler.OnThrowUpdate += SetPowerValue;
            _inputHandler.OnThrowEnd    += (_,_) => ResetPowerValue();
            _inputHandler.OnThrowCancel += ResetPowerValue;
        }


        GameState gameState = GameplayController.Instance?.gameState;
        if (gameState != null)
        {
            //replace default values
            SetFireballValue(gameState.FireballValue);
            SetScore(gameState.Score);
            SetTimeLeft(gameState.RemainingTime);
            SetFireballMultiplierValue(GameConfig.FIREBALL_MULTIPLIER);
            SetFireballMultiplierVisibility(gameState.FireballStatus);

            if (gameState.FireballStatus) SetFireballEnable(); else SetFireballDisable();

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

}
