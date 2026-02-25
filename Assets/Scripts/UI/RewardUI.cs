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
    void DisableScoreUpdates(){
        _gameState.OnScoreChange -= SetFinalScore;
    }
    void Start()
    {
        if (!_gameState) return;
        
        // replace default values
        SetFinalScore(0);

        // score updates
        _gameState.OnScoreChange += SetFinalScore;
        _gameState.OnTimeEnd += DisableScoreUpdates;

        // game over
        if (_enableOnTimeEnd)
            _gameState.OnTimeEnd += ShowUI;
        
    }
}
