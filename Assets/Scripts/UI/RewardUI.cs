using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _finalScore;
    [SerializeField] GameState _gameState;
    [SerializeField] bool _enableOnTimeEnd = false;

    const string _s_finalScore = "{0}";

    void SetFinalScore(int score)
    {
        _finalScore.SetText(_s_finalScore, score);
    }
    void ShowUI()
    {
        GetComponent<Canvas>().enabled = true;
    }
    void SelfDisable()
    {
        if(_enableOnTimeEnd)
            ShowUI();
        enabled = false;
    }

    private void OnEnable()
    {
        if (_gameState)
        {
            // replace default values
            SetFinalScore(_gameState.Score);

            // score updates
            _gameState.OnScoreChange += SetFinalScore;
            _gameState.OnTimeEnd += SelfDisable;
        }
    }
    private void OnDisable()
    {
        if (_gameState)
        {
            _gameState.OnScoreChange -= SetFinalScore;
            _gameState.OnTimeEnd -= SelfDisable;
        }
    }

}
