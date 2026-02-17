using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_GameState : MonoBehaviour
{

    GameState gameState;


    void DBG_Log<T>(T value)
    {
        Debug.Log(value);
    }
    void DBG_Log()
    {
        Debug.Log("EventTriggered");
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = new GameState();
        gameState.OnScoreChange += DBG_Log;
        gameState.AddScore(50);

        gameState.OnFireballValueChange += DBG_Log;
        gameState.OnFireballEnable += DBG_Log;

        for (int i = 0; i < 10; i++)
        {
            gameState.AddFireball(20);
        }
    }
}
