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
        if (!_enableOnTimeEnd) return;
        GetComponent<Canvas>().enabled = true;
    }
    void Start()
    {
        if(_gameState)
        {
            // score updates
            _gameState.OnScoreChange += SetFinalScore;
            
            // game over
            _gameState.OnTimeEnd += ShowUI;
        }
    }
}
