using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// singleton class
public class ScoreController : MonoBehaviour
{
    private static ScoreController _instance;
    public static ScoreController Instance { get { return _instance; } }
    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;
    }

    [SerializeField] private FloaterTextController _floaterText;
    [SerializeField] private ScoreFXController     _scoreFX;


    public void BasketballMiss(GameState state)
    {   
        // what happens to the player if a ball misses
        state.ResetFireball();
    }
    public void BasketballScore(GameState state, int points)
    {
        // what happens to the player if a ball scored
        if (state.FireballStatus) points *= GameConfig.FIREBALL_MULTIPLIER;

        state.AddScore(points);
        state.AddFireball(GameConfig.FIREBALL_INCREMENT);
        _floaterText?.DisplayPoints(points);
        _scoreFX?.PlayFX();
    }

}
