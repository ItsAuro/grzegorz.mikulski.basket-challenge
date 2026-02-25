using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TEST_GameplayUI : MonoBehaviour
{

    int counter = 0;
    bool fireball_flag = false;

    [SerializeField] GameState gameState;

    void Start()
    {
        gameState.StartTimer();
        InvokeRepeating(nameof(TestFireball), 1f, 1f);
    }
    void TestFireball()
    {
        gameState.AddFireball(1);

        if (fireball_flag)
        {
            gameState.ResetFireball();
            fireball_flag = false;
            return;
        }
        if (gameState.FireballStatus)
        {
            fireball_flag = true; 
        }
    }
    void Update()
    {
        // score
        if (counter < 1000)
        {
            gameState.AddScore(1);
            counter++;
        }
        else
        {
            gameState.ResetScore();
            counter = 0;
        }
    }
}
