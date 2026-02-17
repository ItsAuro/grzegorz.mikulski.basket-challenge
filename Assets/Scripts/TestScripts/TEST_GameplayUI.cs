using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_GameplayUI : MonoBehaviour
{

    int counter = 0;
    int fireball_on = 0;
    int fireball_off = 0;
    void Start()
    {
        GameplayController.Instance.StartGame();
    }

    void Update()
    {
        if(fireball_on < 100)
        {
            GameplayController.Instance.gameState.AddFireball(1);
            fireball_on++;

        } else if(fireball_off < 1000){

            fireball_off++;
        }
        else
        {
            GameplayController.Instance.gameState.ResetFireball();
            fireball_on = 0;
            fireball_off = 0;
        }

        GameplayController.Instance.gameState.AddScore(1);
        counter++;
        if (counter >= 1000) {
            GameplayController.Instance.gameState.ResetScore();
            counter = 0;
        }
    }
}
