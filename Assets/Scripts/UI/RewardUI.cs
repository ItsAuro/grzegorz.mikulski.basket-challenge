using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _finalScore;

    const string _s_finalScore = "You scored {0}";

    void SetFinalScore(int score)
    {
        _finalScore.SetText(_s_finalScore, score);
    } 

    void Start()
    {
        if(GameplayController.Instance != null)
        {   
            // could bind to OnGameEnd and implement a way to retrieve score
            GameplayController.Instance.gameState.OnScoreChange += SetFinalScore;
        }
    }
}
