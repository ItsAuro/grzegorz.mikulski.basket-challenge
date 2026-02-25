using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _finalScore;
    [SerializeField] GameState _gameState;

    const string _s_finalScore = "You scored {0}";

    void SetFinalScore(int score)
    {
        _finalScore.SetText(_s_finalScore, score);
    } 

    void Start()
    {
        if(_gameState)
        {
            _gameState.OnScoreChange += SetFinalScore;
        }
    }
}
