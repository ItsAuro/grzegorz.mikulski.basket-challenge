using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI: MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _TMP_Time;
    [SerializeField] TextMeshProUGUI _TMP_FireballMeter;
    [SerializeField] TextMeshProUGUI _TMP_FireballStatus;
    [SerializeField] TextMeshProUGUI _TMP_FireballMultiplier;
    [SerializeField] TextMeshProUGUI _TMP_Score;
    [SerializeField] TextMeshProUGUI _TMP_PowerMeter;
    [SerializeField] Slider _Slider_PowerMeter;
    [SerializeField] Slider _Slider_FireBallMeter;
    [SerializeField] InputHandler _inputHandler;

    [SerializeField] GameState _gameState;
    [SerializeField] bool _disableOnTimeEnd = false;


    const string _s_timeLeft           = "Time\n{0}";
    const string _s_fireballMeter      = "FireballMeter {0}/{1}";
    const string _s_fireballStatus     = "FireballStatus {0}";
    const string _s_score              = "Score {0:D8}";
    const string _s_powerMeter         = "Power {0}/{1}";
    const string _s_fireballMultiplier = "x{0}";


    void HideUI()
    {
        GetComponent<Canvas>().enabled = false;
    }
    void DisableUpdates()
    {
        _gameState.OnFireballValueChange -= SetFireballValue;
        _gameState.OnFireballEnable -= SetFireballEnable;
        _gameState.OnFireballDisable -= SetFireballDisable;
        _gameState.OnScoreChange -= SetScore;
        _gameState.OnRemainingTimeChange -= SetTimeLeft;
    }

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

            _inputHandler.OnSwipeThrowUpdate     += SetPowerValue;
            _inputHandler.OnSwipeThrowSuccessful += (_,_) => ResetPowerValue();
            _inputHandler.OnSwipeThrowCanceled   += ResetPowerValue;
        }

        if (_gameState)
        {
            //replace default values
            SetFireballValue(_gameState.FireballValue);
            SetScore(_gameState.Score);
            SetTimeLeft(_gameState.RemainingTime);
            SetFireballMultiplierValue(GameConfig.FIREBALL_MULTIPLIER);
            SetFireballMultiplierVisibility(_gameState.FireballStatus);

            if (_gameState.FireballStatus) SetFireballEnable(); else SetFireballDisable();

            //fireball updates
            _gameState.OnFireballValueChange += SetFireballValue;
            _gameState.OnFireballEnable += SetFireballEnable;
            _gameState.OnFireballDisable += SetFireballDisable;

            //score updates
            _gameState.OnScoreChange += SetScore;

            //time updates
            _gameState.OnRemainingTimeChange += SetTimeLeft;

            // game over
            _gameState.OnTimeEnd += DisableUpdates;
            if (_disableOnTimeEnd)
                _gameState.OnTimeEnd += HideUI;
        }
 
    }

}
