using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// singleton class
public class GameplayController : MonoBehaviour
{
    private static GameplayController _instance;
    public static GameplayController Instance { get { return _instance; } }
    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;
    }


    public GameState gameState = new GameState();

    public event System.Action OnGameStart;
    public event System.Action OnGameEnd;



    public void StartGame()
    {
        OnGameStart?.Invoke();
        InvokeRepeating(nameof(TimeStep), 1, 1);
    }
    public void EndGame()
    {   
        CancelInvoke(nameof(TimeStep));
        OnGameEnd?.Invoke();
    }

    private void TimeStep()
    {
        gameState.RemoveTime(1);
    }
    void Start()
    {
        gameState.OnTimeEnd += EndGame;
    }

    void Update()
    {
        
    }
}
