using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_GameplayController : MonoBehaviour
{

    void DBG_Log(int value)
    {
        Debug.Log(value);   
    }

    // Start is called before the first frame update
    void Start()
    {
        GameplayController.Instance.gameState.OnRemainingTimeChange += DBG_Log;
        GameplayController.Instance.StartGame();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
