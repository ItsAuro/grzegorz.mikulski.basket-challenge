using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TEST_PlayerArea: MonoBehaviour
{
    [SerializeField] private GameObject spawn;
    [SerializeField] private PlayerArea playerArea;

    int counter = 0;
    void Update()
    {
        if (counter < 100) 
        { 
            counter++;
            return;
        }
        counter = 0;


        Vector3 position = playerArea.GetPoint();
        Instantiate(spawn, position, Quaternion.identity);
    }
}
